using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Models;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Services;
using Training.TruckWorld.Backend.Infrastructure.Filters.Models;

namespace TruckWorld.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IUserService _userService;
    private readonly IUserCredentialsService _userCredentialsService;

    public AccountsController(IAccountService accountService, IUserService userService, IUserCredentialsService userCredentialsService)
    {
        _accountService = accountService;
        _userService = userService;
        _userCredentialsService = userCredentialsService;
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
}
