using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.Responses;

namespace Todo.BusinessLogic.Interfaces;

public interface IIdentityService
{
    Task<AuthResult> SingUp(UserSingUp userData, string basicRoleName = "DefaultUser");
    Task<AuthResult> LogIn(UserLogIn userData);
    Task<AuthResult> RefreshToken(TokenRequest tokenRequest);
}