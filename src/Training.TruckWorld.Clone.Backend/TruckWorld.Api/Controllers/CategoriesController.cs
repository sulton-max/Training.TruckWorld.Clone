using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;

namespace TruckWorld.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IComponentCategoryService _componentCategoryService;
    private readonly ITruckCategoryService _truckCategoryService;

    public CategoriesController(IComponentCategoryService componentCategoryService, ITruckCategoryService truckCategoryService)
    {
        _componentCategoryService = componentCategoryService;
        _truckCategoryService = truckCategoryService;
    }


    [HttpGet("componentCategories")]
    public IActionResult GetAllComponentCategories([FromQuery] int pageToken, [FromQuery] int pageSize)
    {
        var result = _componentCategoryService.Get(componentcategory => true).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
        return result.Any() ? Ok(result) : NotFound();
    }


    [HttpGet("{componentCategoryId:guid}/componentCategory")]
    public async ValueTask<IActionResult> GetComponentCategoryById([FromRoute] Guid componentCategoryId)
    {
        var result = await _componentCategoryService.GetByIdAsync(componentCategoryId);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpPost("componentCategory")]
    public async ValueTask<IActionResult> CreateComponentCategory([FromBody] ComponentCategory componentCategory)
    {
        var result = await _componentCategoryService.CreateAsync(componentCategory);
        return CreatedAtAction(nameof(GetComponentCategoryById), new { componentCategroyId = result.Id }, result);
    }
    

    [HttpPut("componentCategory")]
    public async ValueTask<IActionResult> UpdateComponentCategory([FromBody] ComponentCategory componentCategory)
    {
        var result = await _componentCategoryService.UpdateAsync(componentCategory);
        return NoContent();
    }
    

    [HttpDelete("{componentCategoryId:guid}")]
    public async ValueTask<IActionResult> DeleteComponentCategory([FromRoute] Guid componentCategoryId)
    {
        var result = await _componentCategoryService.DeleteAsync(componentCategoryId);
        return NoContent();
    }

    [HttpGet("truckCategories")]
    public IActionResult GetAllTruckCategories([FromQuery] int pageToken, [FromQuery] int pageSize)
    {
        var result = _truckCategoryService.Get(truckCategory => true).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
        return result.Any() ? Ok(result) : NotFound();
    }


    [HttpGet("{truckCategoryId:guid}/truckCategory")]
    public async ValueTask<IActionResult> GetTruckCategoryById([FromRoute] Guid truckCategoryId)
    {
        var result = await _truckCategoryService.GetByIdAsync(truckCategoryId);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpPost("truckCategory")]
    public async ValueTask<IActionResult> CreateTruckCategory([FromBody] TruckCategory truckCategory)
    {
        var result = await _truckCategoryService.CreateAsync(truckCategory);
        return CreatedAtAction(nameof(GetTruckCategoryById), new { truckCategroyId = result.Id }, result);
    }


    [HttpPut("truckCategory")]
    public async ValueTask<IActionResult> UpdateTruckCategory([FromBody] TruckCategory truckCategory)
    {
        var result = await _truckCategoryService.UpdateAsync(truckCategory);
        return NoContent();
    }


    [HttpDelete("{truckCategoryId:guid}")]
    public async ValueTask<IActionResult> DeleteTruckCategory([FromRoute] Guid truckCategoryId)
    {
        var result = await _truckCategoryService.DeleteAsync(truckCategoryId);
        return NoContent();
    }
}
