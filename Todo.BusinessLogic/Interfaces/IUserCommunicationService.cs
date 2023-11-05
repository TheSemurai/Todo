using Todo.DataAccess.Entities;

namespace Todo.BusinessLogic.Interfaces;

public interface IUserCommunicationService
{
    Task<User?> GetUserById(long id);
}