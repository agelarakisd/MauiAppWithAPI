using MediatR;
using Microsoft.AspNetCore.Mvc;
using PassMaui.Application.Accounts.Commands;
using PassMaui.Application.Accounts.Queries;

namespace PassMaui.API.Controllers;

[Route(Route)]
[ApiController]
public class AccountController : ControllerBase
{
    public const string Route = "api/accounts";
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("", Name = "Create Account")]
    public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountCommand command) => Ok(await _mediator.Send(command));

    [HttpGet("", Name = "Get All Accounts")]
    public async Task<IActionResult> GetAllAccountsAsync() => Ok(await _mediator.Send(new GetAllAccountsQuery()));

    [HttpGet("{id}", Name = "Get Account By Id")]
    public async Task<IActionResult> GetAccountByIdAsync([FromRoute] int id) => Ok(await _mediator.Send(new GetAccountByIdQuery(id)));

    [HttpDelete("{id}", Name = "Delete Account")]
    public async Task<IActionResult> DeleteAccount([FromRoute] int id)
    {
        await _mediator.Send(new DeleteAccountCommand(id));
        return Ok();
    }
}