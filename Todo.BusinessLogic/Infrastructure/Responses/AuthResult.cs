namespace Todo.BusinessLogic.Infrastructure.Responses;

public class AuthResult
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public ICollection<string>? Error { get; set; }
}