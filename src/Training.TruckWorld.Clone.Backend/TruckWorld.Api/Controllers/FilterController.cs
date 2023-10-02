using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Application.Products.Services;
using Training.TruckWorld.Backend.Application.Trucks.Models.Filters;

namespace TruckWorld.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FilterController: ControllerBase
{
    private readonly IFilterService _filterService;
    public FilterController(IFilterService filterService)
    {
        _filterService = filterService;
    }

    [HttpGet("ComponentFilterData")]
    public IActionResult GetComponentFilterDataModel()
    {
        var result = _filterService.GetComponentFilterDataModel();
        return result is not null ? Ok(result) : NotFound();
    }
    [HttpGet("TruckFilterdata")]
    public IActionResult GetTruckFilterDataModel()
    {
        var result = _filterService.GetTruckFilterDataModel();
        return result is not null ? Ok(result) : NotFound();
    }
    [HttpPost("componentfilter")]
    public IActionResult GetFilteredTrucks([FromBody] TruckFilterModel filterModel, [FromQuery] int pageToken, [FromQuery] int pageSize)
    {
        var result = _filterService.GetFilteredTrucks(filterModel, pageSize, pageToken);
        return result is not null ? Ok(result) : NotFound();
    }
    [HttpPost("truckfilter")]
    public IActionResult GetFilteredComponents([FromBody] ComponentFilterModel filterModel, [FromQuery] int pageToken, [FromQuery] int pageSize)
    {
        var result = _filterService.GetFilteredComponents(filterModel, pageSize, pageToken);
        return result is not null ? Ok(result) : NotFound();
    }
}
