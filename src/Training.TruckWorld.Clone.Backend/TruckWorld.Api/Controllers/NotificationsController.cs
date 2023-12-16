using Microsoft.AspNetCore.Mvc;
using TruckWorld.Application.Common.Models.Querying;
using TruckWorld.Application.Common.Notifications.Services;

namespace TruckWorld.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly ISmsTemplateService _smsTemplateService;

    public NotificationsController(IEmailTemplateService emailTemplateService, ISmsTemplateService smsTemplateService)
    {
        _emailTemplateService = emailTemplateService;
        _smsTemplateService = smsTemplateService;
    }

    [HttpGet("smsTemplates")]
    public async ValueTask<IActionResult> GetSmsTemplates([FromQuery] FilterPagination pagination)
    {
        var result = await _smsTemplateService.GetByFilterAsync(pagination);
        return Ok(result);
    }

    [HttpGet("emailTemplates")]
    public async ValueTask<IActionResult> GetEmailTemplates([FromQuery] FilterPagination filterPagination)
    {
        var result = await _emailTemplateService.GetByFilterAsync(filterPagination);
        return Ok(result);
    }
}