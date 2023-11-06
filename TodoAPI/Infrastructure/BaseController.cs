using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Infrastructure;

[ApiController]
[Route("api/[controller]")]
public class BaseController : Controller
{
}