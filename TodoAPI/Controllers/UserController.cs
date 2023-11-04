using Microsoft.AspNetCore.Mvc;
using TodoAPI.Infrastructure;

namespace TodoAPI.Controllers;

public class UserController : BaseController
{
    [HttpGet]
    [Route("GetUser")]
    public async Task<IActionResult> GetUser(long id)
    {
        return Ok(id);
    }
}