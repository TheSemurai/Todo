using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.DataAccess.Entities;
using TodoAPI.Models;

namespace Todo.BusinessLogic.Interfaces;

public interface ITaskService
{
    Task<RequestResult> CreateTask(long userId, CreationTask roleName);
    PersonalTask? GetTaskById(long taskId);
    Task<IEnumerable<PersonalTask>> GetAllTasksByUserAsEnumerable(long userId);
    Task<ICollection<TaskItem>> GetAllTaskToList(long userId);
    Task<RequestResult> RemoveTaskById(long userId, long taskId);
    Task<RequestResult> UpdateTask(long userId, long taskId, TaskItemToUpdate item);
}