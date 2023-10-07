using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Trucks.Models.Filters;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Components.Models;
using Training.TruckWorld.Backend.Infrastructure.Filters.Models;
using Training.TruckWorld.Backend.Infrastructure.Trucks.Models;
using TruckWorld.Api.Models.Dtos;

namespace TruckWorld.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrucksController : ControllerBase
{
    private readonly ITruckService _truckService;
    private readonly ITruckManagementService _truckManagementService;
    private readonly IMapper _mapper;

    public TrucksController(ITruckService truckService, ITruckManagementService truckManagementService, IMapper mapper)
    {
        _truckService = truckService;
        _truckManagementService = truckManagementService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] FilterPagination filterPagination)
    {
        var result = _truckService.Get(truck => true)
            .Skip((filterPagination.PageToken - 1) * filterPagination.PageSize).Take(filterPagination.PageSize)
            .ToList();

        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{truckId:guid}/truck")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid truckId)
    {
        var value = await _truckService.GetByIdAsync(truckId);

        var result = _mapper.Map<TruckDto>(value);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("truckFilterDataModel")]
    public async ValueTask<IActionResult> GetTruckFilterDataModel()
    {
        var result = await _truckService.GetFilterDataModel();
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] TruckDetailsDto truckDetailsDto)
    {
        var truckDetails = new TruckDetails()
        {
            Truck = _mapper.Map<Truck>(truckDetailsDto.TruckDto),
            ContactId = truckDetailsDto.ContactId,
            CategoryId = truckDetailsDto.CategoryId,
            ContactDetails = _mapper.Map<ContactDetails>(truckDetailsDto.ContactDetailsDto)
        };

        var managedTruckDetails = await _truckManagementService.CreateAsync(truckDetails, Guid.Parse("0ed10899-a5e4-4424-848d-51875fa59ead"));

        return managedTruckDetails is not null ? Ok(managedTruckDetails) : BadRequest();
        //return CreatedAtAction(nameof(GetById), new { TruckId = result.TruckDto.Id }, result);
    }

    [HttpPost("truckFilterModel")]
    public async  ValueTask<IActionResult> GetFiltererTrucksAsync([FromBody] TruckFilterModel truckFilterModel)
    {
        var result = await _truckService.GetAsync(truckFilterModel);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateTruck([FromBody] TruckDto truckDto)
    {
        var truck = _mapper.Map<Truck>(truckDto);

        await _truckService.UpdateAsync(truck);

        return NoContent();
    }

    [HttpDelete("{truckId:guid}")]
    public async ValueTask<IActionResult> DeleteTruck([FromRoute] Guid truckId)
    {
        await _truckService.DeleteAsync(truckId);

        return NoContent();
    }
}