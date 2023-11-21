using Microsoft.AspNetCore.Mvc;
using TruckWorld.Application.Common.Models;
using TruckWorld.Application.Common.Services;

namespace TruckWorld.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IEmailSenderService _emailSenderService;
    private readonly ISmsSenderService _smsSenderService;

    public NotificationController(IEmailSenderService emailSenderService, ISmsSenderService smsSenderService)
    {
        _emailSenderService = emailSenderService;
        _smsSenderService = smsSenderService;
    }

    [HttpPut("email")]
    public async ValueTask<IActionResult> SendEmailAsync(EmailMessage emailMessage) =>
        Ok(await _emailSenderService.SendAsync(emailMessage));

    [HttpPut("sms")]
    public async ValueTask<IActionResult> SendSmsAsnyc(SmsMessage smsMessage) =>
        Ok(await _smsSenderService.SendAsync(smsMessage));
}