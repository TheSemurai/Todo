using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.BusinessLogic.Interfaces;
using TodoAPI.Infrastructure;


namespace TodoAPI.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "DefaultUser")]
public class TaskController : BaseController
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    
    [HttpPost]
    [Route("CreateTask")]
    public async Task<IActionResult> CreateTask(CreationTask creationTask)
    {
        long userId;
        
        try
        {
            userId = await User.GetCurrentUser();
        }
        catch (KeyNotFoundException exception)
        {
            return Unauthorized("Current user is not logged, please, login.");
        }
        
        var response = await _taskService.CreateTask(userId, creationTask);

        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet]
    [Route("GetAllTasks")]
    public async Task<IActionResult> GetAllTasks()
    {
        long userId;
        
        try
        {
            userId = await User.GetCurrentUser();
        }
        catch (KeyNotFoundException exception)
        {
            return Unauthorized("Current user is not logged, please, login.");
        }

        var tasks = await _taskService.GetAllTaskToList(userId);
        
        if (!tasks.Any())
            return NoContent();

        return Ok(tasks);
    }

    [HttpDelete]
    [Route("DeleteTask/{taskId}")]
    public async Task<IActionResult> DeleteTask(long taskId)
    {
        long userId;
        
        try
        {
            userId = await User.GetCurrentUser();
        }
        catch (KeyNotFoundException exception)
        {
            return Unauthorized("Current user is not logged, please, login.");
        }

        var response = await _taskService.RemoveTaskById(userId, taskId);

        if (response.Success)
            return Ok(response);
        
        return BadRequest(new RequestResult()
        {
            Success = false,
            Messages = new List<string>()
            {
                $"Server error!"
            }
        });
    }

    [HttpPatch]
    [Route("UpdateTask/{taskId}")]
    public async Task<IActionResult> UpdateTask(long taskId, TaskItemToUpdate item)
    {
        long userId;
        
        try
        {
            userId = await User.GetCurrentUser();
        }
        catch (KeyNotFoundException exception)
        {
            return Unauthorized("Current user is not logged, please, login.");
        }
        
        var response = await _taskService.UpdateTask(userId, taskId, item);
        
        if (response.Success)
            return Ok(response);
        
        return BadRequest(new RequestResult()
        {
            Success = false,
            Messages = new List<string>()
            {
                $"Server error!"
            }
        });
    }
}