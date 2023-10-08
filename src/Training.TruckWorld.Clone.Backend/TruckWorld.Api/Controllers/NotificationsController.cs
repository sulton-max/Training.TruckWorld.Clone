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

    public NotificationsController(IEmailManagementService emailManagementService, IEmailService emailService, IEmailTemplateService emailTemplateService)
    {
        _emailManagementService = emailManagementService;
        _emailService = emailService;
        _emailTemplateService = emailTemplateService;
    }

    [HttpPost("{userId:guid}/{tempateId:guid}")]
    public async ValueTask<IActionResult> SendEmailByTemplateId([FromRoute] Guid userId, Guid templateId)
    {
        Console.WriteLine(templateId);
        var result = await _emailManagementService.SendEmailAsync(userId, templateId);

        return result ? Ok(result) : BadRequest();
    }
    
    [HttpPost("{userId:guid}/{templateCategory}")]
    public async ValueTask<IActionResult> SendEmailTemplateCategory([FromRoute] Guid userId, [FromRoute]string templateCategory)
    {
        var result = await  _emailManagementService.SendEmailAsync(userId, templateCategory);
        return result ? Ok(result) : BadRequest();
    }

    [HttpGet("emails")]
    public IActionResult GetAllEmails([FromQuery] FilterPagination filterPagination)
    {
        
        var result = _emailService.Get(email => true)
            .Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize)
            .ToList();
        
        return result.Any() ? Ok(result) : NotFound();
    }


    [HttpGet("emails/{emailId:guid}/email")]
    public async ValueTask<IActionResult> GetEmailById([FromRoute] Guid emailId)
    {
        var result = await _emailService.GetByIdAsync(emailId);

        return result is not null ? Ok(result) : NotFound();
    }


    [HttpPut("email")]
    public async ValueTask<IActionResult> UpdateEmail([FromBody] Email email)
    {
        var result = await _emailService.UpdateAsync(email);

        return NoContent();
    }


    [HttpDelete("emails/{emailId:guid}")]
    public async ValueTask<IActionResult> DeleteEmail([FromRoute] Guid emailId)
    {
        await _emailService.DeleteAsync(emailId);

        return NoContent();
    }

    [HttpGet("emailTemplates")]
    public IActionResult GetAllEmailTemplates([FromQuery] FilterPagination filterPagination)
    {
        var result = _emailTemplateService.Get(emailTemplate => true)
            .Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize)
            .ToList();

        return result.Any() ? Ok(result) : NotFound();
    }


    [HttpGet("emailTemplates/{emailTemplateId:guid}/emailTemplate")]
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
        await _emailTemplateService.UpdateAsync(emailTemplate);

        return NoContent();
    }


    [HttpDelete("emailTemplates/{emailTemplateId:guid}")]
    public async ValueTask<IActionResult> DeleteEmailTemplate([FromRoute] Guid emailTemplateId)
    {
        await _emailTemplateService.DeleteAsync(emailTemplateId);

        return NoContent();
    }
}