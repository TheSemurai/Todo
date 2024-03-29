using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.BusinessLogic.Interfaces;
using Todo.DataAccess;
using Todo.DataAccess.Entities;

namespace Todo.BusinessLogic.Services.User.Identity;

public class JwtService : IJwtTokenService
{
    private readonly ApplicationContext _context;
    private readonly IConfiguration _configuration;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<DataAccess.Entities.User> _userManager;

    public JwtService(
        ApplicationContext context, 
        IConfiguration configuration, 
        TokenValidationParameters tokenValidationParameters,
        RoleManager<Role> roleManager,
        UserManager<DataAccess.Entities.User> userManager)
    {
        _context = context;
        _configuration = configuration;
        _tokenValidationParameters = tokenValidationParameters;
        _roleManager = roleManager;
        _userManager = userManager;
    }
    
     #region Generation Token fields
     public async Task<AuthResult> GenerateJwtToken(DataAccess.Entities.User user)
     {
         return await CreateJwtTokenByUser(user);
     }
     
     private async Task<AuthResult> CreateJwtTokenByUser(DataAccess.Entities.User user)
     {
         var jwtTokenHandler = new JwtSecurityTokenHandler();
     
         // Getting a secret key and decode it
         var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

         var expiryTimeFrame = TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value);
     
         var claims = await GetFullValidClaims(user);
     
         // Token descriptor
         var tokenDescriptor = new SecurityTokenDescriptor()
         {
             Subject = new ClaimsIdentity(claims),
     
             Expires =  DateTime.UtcNow.Add(expiryTimeFrame),
             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
         };
     
         var token = jwtTokenHandler.CreateToken(tokenDescriptor);
         var jwtToken = jwtTokenHandler.WriteToken(token);
     
         var refreshToken = new RefreshToken()
         {
             JwtId = token.Id,
             Token = RandomStringGeneration(33),
             AddedTime = DateTime.UtcNow,
             ExpiryData = DateTime.UtcNow.AddMonths(3),
             IsRevoked = false,
             IsUsed = false,
             UserId = user.Id
         };
     
         // Save in database
         _context.RefreshTokens.Add(refreshToken);
         await _context.SaveChangesAsync();
     
         return new AuthResult() 
         {
             Token = jwtToken,
             RefreshToken = refreshToken.Token
         };
     }
 
     private async Task<ICollection<Claim>> GetFullValidClaims(DataAccess.Entities.User user)
     {
         var claims = new List<Claim>
         {
             new Claim(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()),
             new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Email),
             new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
             new Claim(type: JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString())
         };
     
         // Getting claims that was assigned to user 
         var userClaims = await _userManager.GetClaimsAsync(user);
         claims.AddRange(userClaims);
     
         // List of roles by specific user
         var userRoles = await _userManager.GetRolesAsync(user);
     
         foreach (var userRole in userRoles)
         {
             var role = await _roleManager.FindByNameAsync(userRole);
         
             if (role != null)
             {
                 claims.Add(new Claim(ClaimTypes.Role, userRole));
                 
                 var roleClaims = await _roleManager.GetClaimsAsync(role);
         
                 foreach (var roleClaim in roleClaims)
                     claims.Add(roleClaim);
             }
         }
     
         return claims;
     }
 
     /// <summary>
     /// Generate random string for refresh token
     /// </summary>
     /// <param name="length">length by string that will be generate</param>
     /// <returns>new random string</returns>
     private string RandomStringGeneration(int length)
     {
         var random = new Random();
         var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_";
     
         var arrayBasedOnChars = 
             Enumerable.Repeat(chars, length)
                 .Select(sel => 
                     sel[random.Next(sel.Length)]).ToArray();
     
         return new string(arrayBasedOnChars);
     }
     #endregion
    
     
     #region RefreshToken fields
     public async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
     {
         return await VerifyAndCreateToken(tokenRequest);
     }
    
     private async Task<AuthResult> VerifyAndCreateToken(TokenRequest tokenRequest)
     {
         var jwtTokenHandler = new JwtSecurityTokenHandler();
 
         try
         {
             var tokenInVerification = 
                 jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);
 
             if (validatedToken is JwtSecurityToken jwtSecurityToken)
             {
                 var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                     StringComparison.InvariantCultureIgnoreCase); //todo: read about the methods and class
 
                 if (result is false)
                     return null;
             }
 
             var utcExprDate = long.Parse(tokenInVerification.Claims
                 .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
 
 
             var expairyDate = UnixTimeStampToDateTime(utcExprDate);
             
             if (expairyDate < DateTime.UtcNow)
                 return new AuthResult()
                 {
                     Error = new List<string>()
                     {
                         "Expired token"
                     }
                 };
 
             var storedToken = await _context
                 .RefreshTokens
                 .FirstOrDefaultAsync(find => 
                     find.Token == tokenRequest.RefreshToken); 
 
             if (storedToken == null)
                 return new AuthResult()
                 {
                     Error = new List<string>()
                     {
                         "Invalid token"
                     }
                 };
             
             if(storedToken.IsUsed)
                 return new AuthResult()
                 {
                     Error = new List<string>()
                     {
                         "Invalid token"
                     }
                 };
 
             if(storedToken.IsRevoked)
                 return new AuthResult()
                 {
                     Error = new List<string>()
                     {
                         "Invalid token"
                     }
                 };
 
             var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
 
             if (storedToken.JwtId != jti)
                 return new AuthResult()
                 {
                     Error = new List<string>()
                     {
                         "Invalid token"
                     }
                 };
 
             if (storedToken.ExpiryData < DateTime.UtcNow)
                 return new AuthResult()
                 {
                     Error = new List<string>()
                     {
                         "Expiry token"
                     }
                 };
        
             storedToken.IsUsed = true;
             _context.RefreshTokens.Update(storedToken);
             await _context.SaveChangesAsync();
             
             var dbUser = await _userManager.FindByIdAsync(storedToken.UserId.ToString());
             return await CreateJwtTokenByUser(dbUser);
         }
         catch (Exception exception)
         {
             Console.WriteLine(exception.Source + '\t' + exception.Message);
             return new AuthResult()
             {
                 Error = new List<string>()
                 {
                     "Server error"
                 }
             };
         }
     }
 
     /// <summary>
     /// Verify time by date
     /// </summary>
     /// <param name="unixTimeStamp">Data argument base on long</param>
     /// <returns>DataTime</returns>
     private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
     {
         var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
         dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
 
         return dateTimeVal;
     }
 
     #endregion
}