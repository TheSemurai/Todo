using Microsoft.AspNetCore.Mvc;
using Todo.BusinessLogic.Entities;
using Todo.BusinessLogic.Infrastructure.Responses;
using Todo.BusinessLogic.Interfaces;
using TodoAPI.Infrastructure;

namespace TodoAPI.Controllers;

public class UserController : BaseController
{
    private readonly IIdentityService _identityService;

    public UserController(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    [HttpPost]
    [Route("SingUp")]
    public async Task<IActionResult> SingUp([FromBody] UserSingUp singUpRequest)
    {
        if (ModelState.IsValid)
        {
            var singUp = await _identityService.SingUp(singUpRequest);
            
            if (singUp.Error is null || !singUp.Error.Any()) 
                return Ok(singUp);
            
            return BadRequest(singUp);
        }
        
        return BadRequest(StatusCode(400));
    }
    
    [HttpPost]
    [Route("LogIn")]
    public async Task<IActionResult> LogIn([FromBody] UserLogIn loginRequest)
    {
        if (ModelState.IsValid)
        {
            var logIn = await _identityService.LogIn(loginRequest);
            
            if (logIn.Error is null || !logIn.Error.Any()) 
                return Ok(logIn);

            return BadRequest(logIn);
        }
        
        return BadRequest(StatusCode(400));
    }
    
    [HttpPost]
    [Route("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
    {
        if (ModelState.IsValid)
        {
            var request = await _identityService.RefreshToken(tokenRequest);
            
            if (request.Error is null || !request.Error.Any()) 
                return Ok(request);
            
            return BadRequest(request);
        }

        return BadRequest(StatusCode(400));
    }
}