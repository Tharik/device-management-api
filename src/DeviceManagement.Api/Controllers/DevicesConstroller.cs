using DeviceManagement.Api.Application.Interfaces;
using DeviceManagement.Api.Contracts.Requests;
using DeviceManagement.Api.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DevicesController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDeviceRequest request)
    {
        var result = await _deviceService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? brand, [FromQuery] DeviceState? state)
    {
        var result = await _deviceService.GetAllAsync(brand, state);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _deviceService.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateDeviceRequest request)
    {
        var result = await _deviceService.UpdateAsync(id, request);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, PatchDeviceRequest request)
    {
        var result = await _deviceService.PatchAsync(id, request);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _deviceService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}