using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckWorld.Api.Models.DTOs;
using TruckWorld.Application.Common.Identity.Services;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IUserService userService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var users = await userService.Get().ToListAsync();
        return users.Any() ? Ok(mapper.Map<IEnumerable<UserDto>>(users)) : NotFound();
    }

    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetUserById([FromRoute] Guid userId)
    {
        var user = await userService.GetByIdAsync(userId);
        return user != null ? Ok(mapper.Map<UserDto>(user)) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateAsync([FromBody] UserDto user)
    {
        var entity = mapper.Map<User>(user);
        var createdUser = await userService.CreateAsync(entity);
        return CreatedAtAction(nameof(GetUserById),
            new
            {
                userId = createdUser.Id,
            },
            mapper.Map<UserDto>(createdUser));
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] UserDto user)
    {
        var entity = mapper.Map<User>(user);
        await userService.UpdateAsync(entity);
        return Ok(entity);
    }

    [HttpDelete("{userId:guid}")]
    public async ValueTask<IActionResult> DeleteByIdAsync([FromRoute] Guid userId)
    {
        await userService.DeleteByIdAsync(userId);
        return Ok();
    }
}
