using System.ComponentModel.DataAnnotations;

namespace Todo.BusinessLogic.Infrastructure.Responses;

public class TokenRequest
{
    [Required] 
    public string Token { get; set; }
    
    [Required] 
    public string RefreshToken { get; set; }
}