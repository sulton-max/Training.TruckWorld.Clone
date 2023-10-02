using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using TruckWorld.Api.Models.Dtos;

namespace TruckWorld.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TruckController: ControllerBase
{
    private readonly ITruckService _truckService;
    public TruckController(ITruckService truckService)
    {
        _truckService = truckService;
    }
    [HttpGet]
    public IActionResult GetAllTrucks([FromQuery] int pageToken, [FromQuery] int pageSize)
    {
        var result = _truckService.Get(user => true).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{truckId:guid}/truck")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid truckId)
    {
        var result = await _truckService.GetByIdAsync(truckId);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateUser([FromBody] TruckDto truckDto)
    {
        var result = await _truckService.UpdateAsync(ToTruck(truckDto));
        return CreatedAtAction(nameof(GetById), new { truckId = result.Id }, result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateUser([FromBody] TruckDto truckDto)
    {
        var result = await _truckService.UpdateAsync(ToTruck(truckDto));
        return NoContent();
    }

    [HttpDelete("{truckId:guid}")]
    public async ValueTask<IActionResult> DeleteUser([FromRoute] Guid truckId)
    {
        var result = await _truckService.DeleteAsync(truckId);
        return NoContent();
    }
    private Truck ToTruck(TruckDto truckDto)
    {
        var ownerId = Guid.NewGuid();
        var truck = new Truck(ownerId, truckDto.SerialNumber, truckDto.Manufacturer, truckDto.Model, truckDto.Category, truckDto.Year, truckDto.Condition, truckDto.Description, truckDto.Price, truckDto.Odometer, truckDto.ListingType, truckDto.EngineType, truckDto.FuelType, truckDto.Color, truckDto.ContactUser);
        return truck;
    }
}
