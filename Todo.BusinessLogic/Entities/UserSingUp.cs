using Todo.DataAccess.Entities;

namespace Todo.BusinessLogic.Entities;

public class UserSingUp
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public GenderEnum Gender { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
}