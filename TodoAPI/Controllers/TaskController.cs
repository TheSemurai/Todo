using Microsoft.AspNetCore.Mvc;
using Todo.BusinessLogic.Interfaces;
using TodoAPI.Infrastructure;
using TodoAPI.Models;

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
}