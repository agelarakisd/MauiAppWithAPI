using Microsoft.AspNetCore.Mvc;
using PassMaui.API.Application;

namespace PassMaui.API.Controllers;

[Route(Route)]
[ApiController]
public class AccountController : ControllerBase
{
    public const string Route = "api/accounts";

    [HttpPost("", Name="Create Account")]
    public IActionResult CreateAccount([FromBody] CreateAccountCommand command)
    {
        return Ok("Created Account");
    }
}