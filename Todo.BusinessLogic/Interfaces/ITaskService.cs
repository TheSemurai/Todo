using Todo.BusinessLogic.Infrastructure.Responses;
using TodoAPI.Models;

namespace Todo.BusinessLogic.Interfaces;

public interface ITaskService
{
    Task<RequestResult> CreateTask(long userId, CreationTask roleName);
}