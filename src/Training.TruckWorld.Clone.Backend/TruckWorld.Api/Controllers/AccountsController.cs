using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Models;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Services;

namespace TruckWorld.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpPost]
    public async ValueTask<IActionResult> Register([FromBody] RegisterDetails regisetrDetails)
    {
        var result = await _accountService.Register(regisetrDetails);
        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPost("account")]
    public async ValueTask<IActionResult> Login([FromBody] LoginDetails loginDetails)
    {
        var result = await _accountService.Login(loginDetails);
        return result is not null ? Ok(result) : BadRequest();
    }
}
