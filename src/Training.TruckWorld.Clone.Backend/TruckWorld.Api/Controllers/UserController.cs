using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;

namespace TruckWorld.Api.Controllers;
[ApiController]
[Route("api/[controller]")]

public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
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
}
