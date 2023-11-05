using Microsoft.AspNetCore.Identity;
using Todo.BusinessLogic.Interfaces;
using Todo.DataAccess;

namespace Todo.BusinessLogic.Services.User;

public class UserCommunicationService : IUserCommunicationService
{
    private readonly ApplicationContext _context;
    private readonly UserManager<DataAccess.Entities.User> _userManager;

    public UserCommunicationService(
        ApplicationContext context, 
        UserManager<DataAccess.Entities.User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<DataAccess.Entities.User?> GetUserById(long id) 
        => _userManager.Users.FirstOrDefault(x => x.Id == id);
    
}