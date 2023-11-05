using System.Linq.Expressions;
using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.ExtensionMethods;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.BusinessLogic.Interfaces;
using Todo.DataAccess;
using Todo.DataAccess.Entities;
using TodoAPI.Models;

namespace Todo.BusinessLogic.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationContext _context;
    private readonly IUserCommunicationService _userCommunicationService;

    public TaskService(
        ApplicationContext context, 
        IUserCommunicationService userCommunicationService)
    {
        _context = context;
        _userCommunicationService = userCommunicationService;
    }
    
    public async Task<RequestResult> CreateTask(long userId, CreationTask creationTask)
    {
        var user = await _userCommunicationService.GetUserById(userId);
        if (user is null)
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    "User cant be null.",
                }
            };

        var task = new PersonalTask()
        {
            Title = creationTask.Title,
            Content = creationTask.Content,
            IsComplete = false,
        };

        try
        {
            user.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return new RequestResult()
            {
                Success = true,
                Messages = new List<string>()
                {
                    "New task has been added."
                }
            };
        }
        catch (Exception exception)
        {
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    "Server error.",
                    exception.Message,
                }
            };
        }
    }

    private IEnumerable<PersonalTask> GetAllTasksAsEnumerable(Expression<Func<PersonalTask?, bool>> predicate) 
        => _context.Tasks.Where(predicate);

    public PersonalTask? GetTaskById(long taskId) 
        => _context.Tasks.FirstOrDefault(x => x.Id == taskId);

    public async Task<IEnumerable<PersonalTask>> GetAllTasksByUserAsEnumerable(long userId) 
        => GetAllTasksAsEnumerable(task => task.Author.Id == userId);

    public async Task<ICollection<TaskItem>> GetAllTaskToList(long userId) 
        => (await GetAllTasksByUserAsEnumerable(userId))
            .Select(x => new TaskItem()
            {
                Id = x.Id,
                Title = x.Title, 
                Content = x.Content, 
                IsComplete = x.IsComplete
            }).ToList();

    public async Task<RequestResult> RemoveTaskById(long userId, long taskId)
    {
        var user = await _userCommunicationService.GetUserById(userId);
        
        if (user is null)
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    $"The user can not be null in method {nameof(RemoveTaskById)}",
                }
            };

        var task = user.Tasks.FirstOrDefault(x => x.Id == taskId);
        
        if (task is null)
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    $"The task has not been found, when tried remove in method {nameof(RemoveTaskById)}",
                }
            };

        try
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            
            return new RequestResult()
            {
                Success = true,
                Messages = new List<string>()
                {
                    $"Task has been removed.",
                    $"Task by id: {taskId} was removed from user by id: {userId}",
                }
            };
        }
        catch (Exception exception)
        {
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    "Server error.",
                    exception.Message,
                }
            };
        }
    }

    public async Task<RequestResult> UpdateTask(long userId, long taskId, TaskItemToUpdate item)
    {
        var olderTask = GetTaskById(taskId);
        
        if(olderTask is null)
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    $"The user can not be null in method {nameof(RemoveTaskById)}",
                }
            };
        
        var updateTask = new PersonalTask()
        {
            Id = taskId,
            Title = item.Title,
            Content = item.Content,
            IsComplete = item.IsComplete ?? false,
            Author = olderTask.Author,
        };
        
        await olderTask.ReplaceOlderByNewTEntityWithoutNullPropertiesAsync(updateTask);

        try
        {
            await _context.SaveChangesAsync();
            _context.Tasks.Update(olderTask);
            
            return new RequestResult()
            {
                Success = true,
                Messages = new List<string>()
                {
                    $"Task has been changed.",
                    $"Task by id: {taskId} was changed.",
                    $"Task by title: {item.Title} was changed.",
                }
            };
        }
        catch (Exception exception)
        {
            return new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    $"Server error. Task can not be changed",
                    exception.Message,
                }
            };
        }
    }
}