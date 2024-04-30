using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Infrastructure;

[ApiController]
[Route("api/[controller]")]
// [EnableCors("CorsPolicy")]
public class BaseController : Controller
{
}