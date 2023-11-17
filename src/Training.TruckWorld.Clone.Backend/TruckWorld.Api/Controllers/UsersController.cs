using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TruckWorld.Api.Models.DTOs;
using TruckWorld.Application.Common.Identity.Services;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var users = await _userService.Get().ToListAsync();
        return users.Any() ? Ok(users) : NotFound();
    }
    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute]Guid userId)
    {
        var user = await _userService.GetByIdAsync(userId);

        return user != null ? Ok(user) : NotFound();
    }
    [HttpPost]
    public async ValueTask<IActionResult> CreateAsync([FromBody]UserDto user)
    {
        var entity = _mapper.Map<UserDto,User>(user);

        var createdUser = await _userService.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById),
            new
            {
                userId = createdUser.Id,
            },
            createdUser);
    }
    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody]User user)
    {
        await _userService.UpdateAsync(user);
        return Ok();
    }
    [HttpDelete("{userId:guid}")]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute]Guid userId)
    {
        await _userService.DeleteByIdAsync(userId);
        return Ok();
    }
}
