using Microsoft.AspNetCore.Identity;
using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.Exceptions;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.BusinessLogic.Interfaces;
using Todo.DataAccess.Configuration;

namespace Todo.BusinessLogic.Services.User.Identity;

public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<DataAccess.Entities.User> _userManager;

    public RoleService(
        RoleManager<Role> roleManager, 
        UserManager<DataAccess.Entities.User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<RequestResult> CreateRole(string name)
    {
        var roleExist = await _roleManager.RoleExistsAsync(name);

        if (roleExist)
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    $"Role exist."
                }
            };
            
        var createRoleResult = await _roleManager.CreateAsync(new Role(name));

        if (createRoleResult.Succeeded)
        {
            //todo:   logger: $"Result of the {nameof(CreateRole)} operation was succeeded!";
            return new RequestResult()
            {
                Success = true,
                Messages = new List<string>()
                {
                    "Operation of create was success!"
                }
            };
        }
        
        //todo: logger: $"Result of the {nameof(CreateRole)} operation was failed."
        return new RequestResult()
        {
            Success = false,
            Messages = new List<string>()
            {
                $"The operation result was failed."
            }
        };
    }

    public async Task<IEnumerable<RestrictedUserInfo>> GetUsersByRole(string name)
    {
        var roleName = await _roleManager.RoleExistsAsync(name);
        if (!roleName) 
            throw new RoleNotFoundException($"Role in {nameof(GetUsersByRole)} are null!");

        var users = await  _userManager.GetUsersInRoleAsync(name);

        var list = new List<RestrictedUserInfo>();

        
        foreach (var user in users)
        {
            var restrictedInfo = new RestrictedUserInfo()
            {
                Email = user.Email,
                Username = user.UserName,
                UserId = user.Id
            };
            
            list.Add(restrictedInfo);
        }

        return list;
    }

    public async Task<IEnumerable<Role>> GetAllRoles() => _roleManager.Roles;

    public async Task<RequestResult> BindUserToRole(string userEmail, string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role != null)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            
            if (user != null)
            {
                var bindUser = await _userManager.AddToRoleAsync(user, roleName);

                if (bindUser.Succeeded)
                {
                    //todo: logger: $"Result of the {nameof(BindUserToRole)} operation was succeeded!";
                    return new RequestResult()
                    {
                        Success = true,
                        Messages = new List<string>()
                        {
                            $"Operation of adding bind between {userEmail} and {roleName} was success!"
                        }    
                    };
                }
                
                //todo: logger: $"Result of the {nameof(BindUserToRole)} operation was failed."
                return new RequestResult()
                {
                    Success = false,
                    Messages = new List<string>()
                    {
                        $"The operation result was failed."
                    }
                };
            }
            
            
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    $"User does not exist."
                }
            };
        }
        
        
        return new RequestResult()
        {
            Success = false,
            Messages = new List<string>()
            {
                $"Role does not exist."
            }
        };
    }

    public async Task<RequestResult> RemoveBind(string userEmail, string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role != null)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            
            if (user != null)
            {
                var removeBind = await _userManager.RemoveFromRoleAsync(user, roleName);

                if (removeBind.Succeeded)
                {
                    //todo: logger: $"Result of the {nameof(BindUserToRole)} operation was succeeded!";
                    return new RequestResult()
                    {
                        Success = true,
                        Messages = new List<string>()
                        {
                            $"Operation of removing bind between {userEmail} and {roleName} was success!"
                        }
                    };
                }
                
                //todo: logger: $"Result of the {nameof(BindUserToRole)} operation was failed."
                return new RequestResult()
                {
                    Success = false,
                    Messages = new List<string>()
                    {
                        $"The operation result was failed."
                    }
                };
            }
            
            
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    $"User does not exist."
                }
            };
        }
        
        
        return new RequestResult()
        {
            Success = false,
            Messages = new List<string>()
            {
                $"Role does not exist."
            }
        };
    }

    public async Task<RequestResult> DeleteRole(string roleName)
    {
        var roles = await GetAllRoles();

        var findRole = roles.Where(x => x.Name == roleName);
        if (!findRole.Any())
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    $"Role does not exist."
                }
            };
        
        var delete = await _roleManager.DeleteAsync(findRole.First());
        if (delete.Succeeded)
            return new RequestResult()
            {
                Success = true,
                Messages = new List<string>()
                {
                    $"The removed role operation with name: {roleName} was success!"
                }
            };
        
        
        return new RequestResult()
        {
            Success = false,
            Messages = new List<string>()
            {
                $"The removed role operation with name: {roleName} was failed!"
            }
        };
    }
}