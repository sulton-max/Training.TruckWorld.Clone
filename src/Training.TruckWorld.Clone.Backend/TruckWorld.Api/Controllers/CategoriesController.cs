using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Filters.Models;

namespace TruckWorld.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IComponentCategoryService _componentCategoryService;
    private readonly ITruckCategoryService _truckCategoryService;

    public CategoriesController(IComponentCategoryService componentCategoryService,
        ITruckCategoryService truckCategoryService)
    {
        _componentCategoryService = componentCategoryService;
        _truckCategoryService = truckCategoryService;
    }


    [HttpGet("componentCategories")]
    public IActionResult GetAllComponentCategories([FromQuery] FilterPagination filterPagination)
    {
        var result = _componentCategoryService.Get(componentcategory => true)
            .Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize)
            .ToList();
        return result.Any() ? Ok(result) : NotFound();
    }


    [HttpGet("{componentCategoryId:guid}/componentCategory")]
    public async ValueTask<IActionResult> GetComponentCategoryById([FromRoute] Guid componentCategoryId)
    {
        var result = await _componentCategoryService.GetByIdAsync(componentCategoryId);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost("componentCategory")]
    [ActionName(nameof(GetComponentCategoryById))]
    public async ValueTask<IActionResult> CreateComponentCategory([FromBody] ComponentCategory componentCategory)
    {
        var result = await _componentCategoryService.CreateAsync(componentCategory);

        return await GetComponentCategoryById(result.Id);
    }


    [HttpPut("componentCategory")]
    public async ValueTask<IActionResult> UpdateComponentCategoryAsync([FromBody] ComponentCategory componentCategory)
    {
        await _componentCategoryService.UpdateAsync(componentCategory);

        return NoContent();
    }


    [HttpDelete("{componentCategoryId:guid}/componentCategory")]
    public async ValueTask<IActionResult> DeleteComponentCategoryAsync([FromRoute] Guid componentCategoryId)
    {
        await _componentCategoryService.DeleteAsync(componentCategoryId);

        return NoContent();
    }

    [HttpGet("truckCategories")]
    public IActionResult GetAllTruckCategories([FromQuery] FilterPagination filterPagination)
    {
        var result = _truckCategoryService.Get(truckCategory => true)
            .Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize)
            .ToList();
        return result.Any() ? Ok(result) : NotFound();
    }


    [HttpGet("{truckCategoryId:guid}/truckCategory")]
    public async ValueTask<IActionResult> GetTruckCategoryById([FromRoute] Guid truckCategoryId)
    {
        var result = await _truckCategoryService.GetByIdAsync(truckCategoryId);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpPost("truckCategory")]
    [ActionName(nameof(GetTruckCategoryByIdAsync))]
    public async ValueTask<IActionResult> CreateTruckCategory([FromBody] TruckCategory truckCategory)
    {
        var result = await _truckCategoryService.CreateAsync(truckCategory);
        return await GetTruckCategoryById(result.Id);
    }


    [HttpPut("truckCategory")]
    public async ValueTask<IActionResult> UpdateTruckCategoryAsync([FromBody] TruckCategory truckCategory)
    {
        await _truckCategoryService.UpdateAsync(truckCategory);

        return NoContent();
    }


    [HttpDelete("{truckCategoryId:guid}/truckCategory")]
    public async ValueTask<IActionResult> DeleteTruckCategoryAsync([FromRoute] Guid truckCategoryId)
    {
        await _truckCategoryService.DeleteAsync(truckCategoryId);

        return NoContent();
    }
}