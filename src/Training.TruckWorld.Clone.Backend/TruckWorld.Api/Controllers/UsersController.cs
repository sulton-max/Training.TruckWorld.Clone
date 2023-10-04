using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;

namespace TruckWorld.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserCredentialsService _userCredentialsService;

    public UsersController(IUserService userService, IUserCredentialsService userCredentialsService)
    {
        _userService = userService;
        _userCredentialsService = userCredentialsService;
    }

    [HttpGet]
    public IActionResult GetAllUsers([FromQuery] int pageToken, [FromQuery] int pageSize)
    {
        var result = _userService.Get(user => true).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{userId:guid}/user")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid userId)
    {
        var result = await _userService.GetByIdAsync(userId);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateUser([FromBody] User user)
    {
        var result = await _userService.CreateAsync(user);
        return CreatedAtAction(nameof(GetById), new { userId = result.Id }, result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateUser([FromBody] User user)
    {
        var result = await _userService.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{userId:guid}")]
    public async ValueTask<IActionResult> DeleteUser([FromRoute] Guid userId)
    {
        var result = await _userService.DeleteAsync(userId);
        return NoContent();
    }

    [HttpGet("credentials")]
    public IActionResult GetAllCredentials([FromQuery] int pageToken, [FromQuery] int pageSize)
    {
        var result = _userCredentialsService.Get(user => true).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{credentialsId:guid}/credentials")]
    public async ValueTask<IActionResult> GetCredentialsById([FromRoute] Guid credentialsId)
    {
        var result = await _userCredentialsService.GetByIdAsync(credentialsId);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("{userId:guid}/credentials")]
    public IActionResult GetCredentialsByUserId([FromRoute] Guid userId)
    {
        var result = _userCredentialsService.Get(credentials => credentials.UserId == userId).First();
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost("credentials")]
    public async ValueTask<IActionResult> CreateCredentials([FromBody] UserCredentials userCredentials)
    {
        var result = await _userCredentialsService.CreateAsync(userCredentials);
        return CreatedAtAction(nameof(GetById), new { userId = result.Id }, result);
    }

    [HttpPut("oldPassword/credentials")]
    public async ValueTask<IActionResult> UpdateCredentials(string oldPassword,
        [FromBody] UserCredentials userCredentials)
    {
        var result = await _userCredentialsService.UpdateAsync(oldPassword, userCredentials);
        return NoContent();
    }

    [HttpDelete("{crdentialsId:guid}")]
    public async ValueTask<IActionResult> DeleteCredentials([FromRoute] Guid credentialsId)
    {
        var result = await _userCredentialsService.DeleteAsync(credentialsId);
        return NoContent();
    }
}