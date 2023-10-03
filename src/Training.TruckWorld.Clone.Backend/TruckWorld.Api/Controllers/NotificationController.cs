using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Notifications.Services;

namespace TruckWorld.Api.Controllers;
[ApiController]
[Route("api/[controller]")]

public class EmailManagementController : ControllerBase
{
    private readonly IEmailManagementService _emailManagementService;
    public EmailManagementController(IEmailManagementService emailManagementService)
    {
        _emailManagementService = emailManagementService;
    }

    [HttpPost("{userId : Guid, tempate : Guid}")]
    public async ValueTask<IActionResult> SendEmailAsync(Guid userId, Guid templateId)
    {
        var result = await _emailManagementService.SendEmailAsync(userId, templateId);
        return result ? Ok(result) : BadRequest();
    }
    [HttpPost("{userId : Guid, templateCategory : string}")]
    public IActionResult SendEmailAsync(Guid userId, string templateCategory)
    {
        var result = _emailManagementService.SendEmailAsync(userId, templateCategory);
        return result.Any() ? Ok(result) : BadRequest();
    }
}
