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

    [HttpPost("productFilterModel")]
    public IActionResult GetFilteredProducts([FromBody]ProductFilterModel productFilterModel)
    {
        var result = _filterService.GetFilteredProducts(productFilterModel);
        return result.Any() ? Ok(result) : NotFound();
    }
}
