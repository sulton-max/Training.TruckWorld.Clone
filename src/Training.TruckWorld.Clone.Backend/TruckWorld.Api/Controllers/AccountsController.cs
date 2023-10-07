using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Models;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Services;
using Training.TruckWorld.Backend.Infrastructure.Filters.Models;
using TruckWorld.Api.Models.Dtos;

namespace TruckWorld.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IUserService _userService;
    private readonly IUserCredentialsService _userCredentialsService;
    private readonly IContactService _contactService;
    private readonly IMapper _mapper; 

    public AccountsController(IAccountService accountService, IUserService userService, IUserCredentialsService userCredentialsService, IContactService contactService, IMapper mapper)
    {
        _accountService = accountService;
        _userService = userService;
        _userCredentialsService = userCredentialsService;
        _contactService = contactService;
        _mapper = mapper;
    }
    [HttpPost("register")]
    public async ValueTask<IActionResult> Register([FromBody] RegisterDetails regisetrDetails)
    {
        var result = await _accountService.Register(regisetrDetails);
        
        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPost("login")]
    public async ValueTask<IActionResult> Login([FromBody] LoginDetails loginDetails)
    {
        var result = await _accountService.Login(loginDetails);
        
        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpGet("users")]
    public IActionResult GetAllUsers([FromQuery] FilterPagination filterPagination)
    {
        var result = _userService.Get(user => true).Skip((filterPagination.PageToken - 1) * filterPagination.PageSize)
            .Take(filterPagination.PageSize).ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("users/{userId:guid}/user")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid userId)
    {
        var result = await _userService.GetByIdAsync(userId);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPut("user")]
    public async ValueTask<IActionResult> UpdateUser([FromBody] User user)
    {
        var result = await _userService.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("users/{userId:guid}")]
    public async ValueTask<IActionResult> DeleteUser([FromRoute] Guid userId)
    {
        var result = await _userService.DeleteAsync(userId);
        return NoContent();
    }

    [HttpGet("credentials")]
    public IActionResult GetAllCredentials([FromQuery] FilterPagination filterPagination)
    {
        var result = _userCredentialsService.Get(user => true)
            .Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize)
            .ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("credentials/{credentialsId:guid}/credentials")]
    public async ValueTask<IActionResult> GetCredentialsById([FromRoute] Guid credentialsId)
    {
        var result = await _userCredentialsService.GetByIdAsync(credentialsId);
        Console.WriteLine(result is null);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("{userId:guid}/credentials")]
    public IActionResult GetCredentialsByUserId([FromRoute] Guid userId)
    {
        var result = _userCredentialsService.Get(credentials => credentials.UserId == userId).First();
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPut("credentials/oldPassword/credentials")]
    public async ValueTask<IActionResult> UpdateCredentials(string oldPassword,
        [FromBody] UserCredentials userCredentials)
    {
        var result = await _userCredentialsService.UpdateAsync(oldPassword, userCredentials);
        return NoContent();
    }

    [HttpDelete("credentials/{credentialsId:guid}")]
    public async ValueTask<IActionResult> DeleteCredentials([FromRoute] Guid credentialsId)
    {
        var result = await _userCredentialsService.DeleteAsync(credentialsId);
        return NoContent();
    }

    [HttpGet("contacts")]
    public IActionResult GetAllContacts([FromQuery] FilterPagination filterPagination)
    {
        var result = _contactService.Get(user => true)
            .Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize)
            .ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("contacts/{contactId:guid}/contact")]
    public async ValueTask<IActionResult> GetContactById([FromRoute] Guid contactId)
    {
        var result = await _contactService.GetByIdAsync(contactId);
        Console.WriteLine(result is null);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost("contacts/contact")]
    public async ValueTask<IActionResult> CreateContact(ContactDetailsDto contact)
    {
        var value = await _contactService.CreateAsync(_mapper.Map<ContactDetails>(contact));
        var result = _mapper.Map<ContactDetailsDto>(value);
        return CreatedAtAction(nameof(GetContactById), new { ContactId = contact.Id}, result);
    }

    [HttpPut("contacts/contact")]
    public async ValueTask<IActionResult> UpdateContact([FromBody] ContactDetailsDto contactDetailsDto)
    {
        var result = await _contactService.UpdateAsync(_mapper.Map<ContactDetails>(contactDetailsDto));
        return NoContent();
    }

    [HttpDelete("contacts/{contactId:guid}")]
    public async ValueTask<IActionResult> DeleteContacts([FromRoute] Guid contactId)
    {
        var result = await _contactService.DeleteAsync(contactId);
        return NoContent();
    }
}