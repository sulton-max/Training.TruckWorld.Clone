using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckWorld.Api.Models.DTOs;
using TruckWorld.Application.Common.Identity.Services;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Api.Controllers;

/// <summary>
/// API controller for managing user accounts.
/// </summary>
/// <param name="userService"></param>
/// <param name="mapper"></param>
[Route("api/[controller]")]
[ApiController]
public class AccountsController(IUserService userService, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var users = await userService.Get().ToListAsync();
        return users.Any() ? Ok(users) : NotFound();
    }

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetUserById([FromRoute] Guid userId)
    {
        var user = await userService.GetByIdAsync(userId);
        return user != null ? Ok(user) : NotFound();
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async ValueTask<IActionResult> CreateAsync([FromBody] UserDto user)
    {
        var entity = mapper.Map<UserDto, User>(user);
        var createdUser = await userService.CreateAsync(entity);
        return CreatedAtAction(nameof(GetUserById),
            new
            {
                userId = createdUser.Id,
            },
            createdUser);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] User user)
    {
        await userService.UpdateAsync(user);
        return Ok();
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete("{userId:guid}")]
    public async ValueTask<IActionResult> DeleteByIdAsync([FromRoute] Guid userId)
    {
        await userService.DeleteByIdAsync(userId);
        return Ok();
    }

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpDelete]
    public async ValueTask<IActionResult> DeleteAsync([FromBody] User user)
    {
        await userService.DeleteAsync(user);
        return Ok();
    }
}
