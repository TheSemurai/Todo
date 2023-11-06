using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.DataAccess.Entities;

namespace Todo.BusinessLogic.Interfaces;

public interface IJwtTokenService
{
    public Task<AuthResult> GenerateJwtToken(User user);
    public Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest);
}