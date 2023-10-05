using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Notifications.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Filters.Models;

namespace TruckWorld.Api.Controllers;
[ApiController]
[Route("api/[controller]")]

public class NotificationsController : ControllerBase
{
    private readonly IEmailManagementService _emailManagementService;
    private readonly IEmailService _emailService;
    private readonly IEmailTemplateService _emailTemplateService;

    public NotificationsController(IEmailManagementService emailManagementService)
    {
        _emailManagementService = emailManagementService;
    }

    [HttpPost("{userId:guid}/{tempateId:Guid}")]
    public async ValueTask<IActionResult> SendEmailAsync(Guid userId, Guid templateId)
    {
        var result = await _emailManagementService.SendEmailAsync(userId, templateId);
        return result ? Ok(result) : BadRequest();
    }

    [HttpPost("{userId:guid}/{templateCategory}")]
    public IActionResult SendEmailAsync(Guid userId, string templateCategory)
    {
        var result = _emailManagementService.SendEmailAsync(userId, templateCategory);
        return result.Any() ? Ok(result) : BadRequest();
    }

    [HttpGet("emails")]
    public IActionResult GetAllEmails([FromQuery] FilterPagination filterPagination)
    {
        var result = _emailService.Get(email => true).Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize).ToList();
        return result.Any() ? Ok(result) : NotFound();
    }


    [HttpGet("{emailId:guid}/email")]
    public async ValueTask<IActionResult> GetEmailById([FromRoute] Guid emailId)
    {
        var result = await _emailService.GetByIdAsync(emailId);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpPost("email")]
    public async ValueTask<IActionResult> CreateEmail([FromBody] Email email)
    {
        var result = await _emailService.CreateAsync(email);
        return CreatedAtAction(nameof(GetEmailById), new { emailId = result.Id }, result);
    }


    [HttpPut("email")]
    public async ValueTask<IActionResult> UpdateEmail([FromBody] Email email)
    {
        var result = await _emailService.UpdateAsync(email);
        return NoContent();
    }


    [HttpDelete("{emailId:guid}")]
    public async ValueTask<IActionResult> DeleteEmail([FromRoute] Guid emailId)
    {
        var result = await _emailService.DeleteAsync(emailId);
        return NoContent();
    }

    [HttpGet("emailTemplates")]
    public IActionResult GetAllEmailTemplates([FromQuery] FilterPagination filterPagination)
    {
        var result = _emailTemplateService.Get(emailTemplate => true).Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize).ToList();
        return result.Any() ? Ok(result) : NotFound();
    }


    [HttpGet("{emailTemplateId:guid}/emailTemplate")]
    public async ValueTask<IActionResult> GetEmailTemplateById([FromRoute] Guid emailTemplateId)
    {
        var result = await _emailTemplateService.GetByIdAsync(emailTemplateId);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpPost("emailTemplate")]
    public async ValueTask<IActionResult> CreateTRuckCategory([FromBody] EmailTemplate emailTemplate)
    {
        var result = await _emailTemplateService.CreateAsync(emailTemplate);
        return CreatedAtAction(nameof(GetEmailTemplateById), new { emailTemplateId = result.Id }, result);
    }


    [HttpPut("emailTemplate")]
    public async ValueTask<IActionResult> UpdateEmailTemplate([FromBody] EmailTemplate emailTemplate)
    {
        var result = await _emailTemplateService.UpdateAsync(emailTemplate);
        return NoContent();
    }


    [HttpDelete("{emailTemplateId:guid}")]
    public async ValueTask<IActionResult> DeleteEmailTemplate([FromRoute] Guid emailTemplateId)
    {
        var result = await _emailTemplateService.DeleteAsync(emailTemplateId);
        return NoContent();
    }
}
