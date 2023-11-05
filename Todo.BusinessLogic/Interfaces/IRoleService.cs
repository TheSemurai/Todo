using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.DataAccess.Configuration;

namespace Todo.BusinessLogic.Interfaces;

public interface IRoleService
{
    Task<RequestResult> CreateRole(string roleName);
    Task<IEnumerable<RestrictedUserInfo>> GetUsersByRole(string roleName);
    Task<IEnumerable<Role>> GetAllRoles();
    Task<RequestResult> BindUserToRole(string userEmail, string roleName);
    Task<RequestResult> RemoveBind(string userEmail, string roleName);
    Task<RequestResult> DeleteRole(string roleName);
}