using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Service.Models;
using TicketFlow.Service.UserCase.V1.Profile;

namespace TicketFlow.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProfileController: ControllerBase
{
    private readonly IService _service;

    public ProfileController(IService service)
    {
        _service = service;
    }
    [Authorize]
    [HttpPut("/me")]
    public async Task<IActionResult> UpdateProfile(Request.UpdateProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        await _service.UpdateProfile(request, cancellationToken);
        return Ok(ApiResponseFactory.SuccessResponse("Updated Profile Success", null, HttpContext.TraceIdentifier));
    }
}