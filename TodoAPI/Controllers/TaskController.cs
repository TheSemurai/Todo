using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.BusinessLogic.Interfaces;
using TodoAPI.Infrastructure;
using TodoAPI.Models;

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
        var userId = await User.GetCurrentUser();
        
        var response = await _taskService.CreateTask(userId, creationTask);

        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet]
    [Route("GetAllTasks")]
    public async Task<IActionResult> GetAllTasks()
    {
        try
        {
            var userId = await User.GetCurrentUser();
            
            var tasks = await _taskService.GetAllTaskToList(userId);
        
            if (!tasks.Any())
                return NoContent();

            return Ok(tasks);
        }
        catch (Exception exception)
        {
            return BadRequest(new RequestResult()
            {
                Success = false,
                Messages = new List<string>()
                {
                    $"Server error!",
                    exception.Message,
                }
            });
        }

    }

    [HttpDelete]
    [Route("DeleteTask/{taskId}")]
    public async Task<IActionResult> DeleteTask(long taskId)
    {
        var userId = await User.GetCurrentUser();

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
        var userId = await User.GetCurrentUser();
        
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