using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using TruckWorld.Api.Models.Dtos;

namespace TruckWorld.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TrucksController: ControllerBase
{
    private readonly ITruckService _truckService;
    private readonly IMapper _mapper;
    public TrucksController(ITruckService truckService, IMapper mapper)
    {
        _truckService = truckService;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult GetAllTrucks([FromQuery] int pageToken, [FromQuery] int pageSize)
    {
        var value = _truckService.Get(user => true).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
        var result = _mapper.Map<List<TruckDto>>(value);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{truckId:guid}/truck")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid truckId)
    {
        var value = await _truckService.GetByIdAsync(truckId);
        var result = _mapper.Map<TruckDto>(value);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateTruck([FromBody] TruckDto truckDto)
    {
        var truck = _mapper.Map<Truck>(truckDto);
        var value = await _truckService.CreateAsync(truck);
        var result = _mapper.Map<TruckDto>(value);
        return CreatedAtAction(nameof(GetById), new { truckId = result.Id }, result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateTruck([FromBody] TruckDto truckDto)
    {
        var truck = _mapper.Map<Truck>(truckDto);
        var value = await _truckService.UpdateAsync(truck);
        var result = _mapper.Map<TruckDto>(value);
        return NoContent();
    }

    [HttpDelete("{truckId:guid}")]
    public async ValueTask<IActionResult> DeleteTruck([FromRoute] Guid truckId)
    {
        var value = await _truckService.DeleteAsync(truckId);
        var result = _mapper.Map<TruckDto>(value);
        return NoContent();
    }
}
