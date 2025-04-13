using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult GetUsers()
    {
        return Ok(new { message = "This is a protected endpoint returning user data." });
    }
}
