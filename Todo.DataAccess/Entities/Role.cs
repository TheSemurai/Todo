using Microsoft.AspNetCore.Identity;

namespace Todo.DataAccess.Configuration;

public class Role : IdentityRole<long>
{
    public Role() : base(){}

    public Role(string roleName) : base(roleName)
    {
        
    }
}