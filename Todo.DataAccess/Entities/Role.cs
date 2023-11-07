using Microsoft.AspNetCore.Identity;

namespace Todo.DataAccess.Entities;

public class Role : IdentityRole<long>
{
    public Role() : base(){}

    public Role(string roleName) : base(roleName)
    {
        
    }
}