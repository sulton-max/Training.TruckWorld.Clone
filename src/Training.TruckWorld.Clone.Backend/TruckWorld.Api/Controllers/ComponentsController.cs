using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Filters.Models;
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
    public IActionResult GetAll([FromQuery] FilterPagination filterPagination)
    {
        var value = _componentService.Get(user => true)
            .Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize)
            .ToList();

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

    [HttpGet("componentFilterDataModel")]
    public async ValueTask<IActionResult> GetFilterDataModel()
    {
        var result = await _componentService.GetFilterDataModel();
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] ComponentDto componentDto)
    {
        var component = _mapper.Map<Component>(componentDto);

        component.UserId = Guid.Parse("0ed10899-a5e4-4424-848d-51875fa59ead");

        var value = await _componentService.CreateAsync(component);

        var result = _mapper.Map<ComponentDto>(value);

        return CreatedAtAction(nameof(GetById), new { componentId = result.Id }, result);
    }

    [HttpPost("componentFilterModel")]
    public async ValueTask<IActionResult> GetFiltered([FromBody] ComponentFilterModel componentFilterModel)
    {
        var result = await _componentService.GetAsync(componentFilterModel);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] ComponentDto componentDto)
    {
        var component = _mapper.Map<Component>(componentDto);
        component.UserId = Guid.Parse("0ed10899-a5e4-4424-848d-51875fa59ead");

        var value = _componentService.UpdateAsync(component);
        
        var result = _mapper.Map<ComponentDto>(value);
        
        return CreatedAtAction(nameof(GetById), new { componentId = result.Id }, result);
    }

    [HttpDelete("{componentId:guid}")]
    public async ValueTask<IActionResult> Delete([FromRoute] Guid componentId)
    {
        var value = await _componentService.DeleteAsync(componentId);

        var result = _mapper.Map<ComponentDto>(value);

        return NoContent();
    }
}