using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using TruckWorld.Api.Models.Dtos;

namespace TruckWorld.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComponentsController : ControllerBase
{
    private readonly IComponentService _componentService;
    private readonly IMapper _mapper;
    public ComponentsController(IComponentService componentService, IMapper mapper)
    {
        _componentService = componentService;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult GetAllComponents([FromQuery] int pageToken, [FromQuery] int pageSize)
    {
        var value = _componentService.Get(user => true).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
        var result = _mapper.Map<List<ComponentDto>>(value);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{componentId:guid}/component")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid componentId)
    {
        var value = await _componentService.GetByIdAsync(componentId);
        var result = _mapper.Map<ComponentDto>(value);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateComponent([FromBody] ComponentDto componentDto)
    {
        var component = _mapper.Map<Component>(componentDto);
        var value = await _componentService.CreateAsync(component);
        var result = _mapper.Map<ComponentDto>(value);
        return CreatedAtAction(nameof(GetById), new { componentId = result.Id }, result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateComponent([FromBody] ComponentDto componentDto)
    {
        var component = _mapper.Map<Component>(componentDto);
        var value = await _componentService.UpdateAsync(component);
        var result = _mapper.Map<TruckDto>(value);
        return NoContent();
    }

    [HttpDelete("{componentId:guid}")]
    public async ValueTask<IActionResult> DeleteComponent([FromRoute] Guid componentId)
    {
        var value = await _componentService.DeleteAsync(componentId);
        var result = _mapper.Map<ComponentDto>(value);
        return NoContent();
    }
}

