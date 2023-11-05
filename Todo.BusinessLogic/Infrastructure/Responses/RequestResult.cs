namespace Todo.BusinessLogic.Infrastructure.Responses;

public class RequestResult
{
    public bool Success { get; set; }
    public List<string>? Messages  { get; set; }
}