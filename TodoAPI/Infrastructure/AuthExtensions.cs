using System.Security.Claims;

namespace TodoAPI.Infrastructure;

public static class AuthExtensions
{
    public static async Task<long> GetCurrentUser(this ClaimsPrincipal claims)
    {
        var userId = 
            claims.FindFirst(ClaimTypes.NameIdentifier)?.Value 
            ?? 
            throw new ApplicationException("Something went wrong.");
        
        return long.Parse(userId);
    }
}