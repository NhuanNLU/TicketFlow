using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Service.Models;
using TicketFlow.Service.UserCase.V1.Profile;

namespace TicketFlow.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController: ControllerBase
{
    private readonly IService _service;
    public ProfileController(IService service)
    {
        _service = service;
    }
    [HttpGet("me")]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken = default)
    {
        var result = await _service.GetProfile(cancellationToken);
        return Ok(ApiResponseFactory.SuccessResponse("Get Profile Success", result, HttpContext.TraceIdentifier));
    }
    [HttpPut("me")]
    public async Task<IActionResult> UpdateProfile(Request.UpdateProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        await _service.UpdateProfile(request, cancellationToken);
        return Ok(ApiResponseFactory.SuccessResponse("Updated Profile Success", null, HttpContext.TraceIdentifier));
    }
}