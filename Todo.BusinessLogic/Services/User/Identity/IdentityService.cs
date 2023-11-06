using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.BusinessLogic.Interfaces;

namespace Todo.BusinessLogic.Services.User.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<DataAccess.Entities.User> _userManager;
    private readonly IRoleService _roleService;
    private readonly IJwtTokenService _jwtService;

    public IdentityService(
        UserManager<DataAccess.Entities.User> userManager, 
        IRoleService roleService, 
        IJwtTokenService jwtService)
    {
        _userManager = userManager;
        _roleService = roleService;
        _jwtService = jwtService;
    }

    private async Task<bool> EmailIsExistInDatabase(string email) 
        => await _userManager.FindByEmailAsync(email) != null;

    public async Task<AuthResult> SingUp(UserSingUp userData, string basicRoleName)
    {
        var emailExist = await EmailIsExistInDatabase(userData.Email);
        if (emailExist)
            return new AuthResult()
            {
                Error = new List<string>()
                {
                    "Occur error. Email already exist!"
                }
            };
        
        var user = new DataAccess.Entities.User()
        {
            Email = userData.Email,
            Gender = userData.Gender,
            Name = userData.Name,
            Lastname = userData.Lastname,
            UserName = userData.Username,
        };
        
        var isCreated = await _userManager.CreateAsync(user, userData.Password);
        if (isCreated.Succeeded)
        {
            // Creation and bind user with basic role
            await CreateRoleIfNotExist(basicRoleName);
            await _userManager.AddToRoleAsync(user, basicRoleName);
            
            // Generate the token
            var jwtToken = await _jwtService.GenerateJwtToken(user);
            
            return jwtToken;
        }

        return new AuthResult()
        {
            Error = new List<string>()
            {
                "An error occurred while registering on the server. Account has not been created"
            }
        };
    }
    
    private async Task CreateRoleIfNotExist(string name)
    {
        var roleExist = await _roleService.IsRoleExist(name);

        if (!roleExist)
            await _roleService.CreateRole(name);
    }
    
    public async Task<AuthResult> LogIn(UserLogIn userData)
    {
        var existingUser = await _userManager.FindByEmailAsync(userData.Email);

        if (existingUser == null)
            return new AuthResult()
            {
                Error = new List<string>()
                {
                    "Something went wrong, error of email"
                },
            };

        var isCorrect = await _userManager.CheckPasswordAsync(existingUser, userData.Password);
        
        if(!isCorrect)
            return new AuthResult()
            {
                Error = new List<string>()
                {
                    "Something went wrong, please check the accuracy of your data: email or password"
                }
            };

        //var jwtToken = await _jwtService.GenerateJwtToken(existingUser);
        
        //return jwtToken;
        return new AuthResult()
        {
            Token = "lol-works",
            RefreshToken = "works!"
        };
    }
}