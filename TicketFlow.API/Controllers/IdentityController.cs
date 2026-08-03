using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Service.UserCase.V1.Commands.Identity.Register;
using TicketFlow.Service.UserCase.V1.Queries.Identity.Login;

namespace TicketFlow.API.Controllers;
[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class IdentityController: ControllerBase
{
    private readonly ISender _sender;
    public IdentityController(ISender sender)
    {
        _sender = sender;
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterCommand request)
    {
        var result = await _sender.Send(request);
        return Ok(result);
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequestQuery request)
    {
        var result = await _sender.Send(request);
        return Ok(result);
    }
}