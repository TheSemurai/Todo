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
    
    
}