using Microsoft.AspNetCore.Mvc;
using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.BusinessLogic.Interfaces;
using TodoAPI.Infrastructure;
using TodoAPI.Models;
using TaskItem = Todo.BusinessLogic.Entities.TaskItem;

namespace TodoAPI.Controllers;

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
        var response = await _taskService.CreateTask(10, creationTask); // todo: Hard code id

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
            var tasks = await _taskService.GetAllTaskToList(10);
        
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
    [Route("DeleteTask")]
    public async Task<IActionResult> DeleteTask(long taskId)
    {
        var response = await _taskService.RemoveTaskById(10, taskId);

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
        var response = await _taskService.UpdateTask(10, taskId, item);
        
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