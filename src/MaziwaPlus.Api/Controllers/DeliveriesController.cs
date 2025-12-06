using Microsoft.AspNetCore.Mvc;
using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Api.Services;

namespace MaziwaPlus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveriesController : ControllerBase
{
    private readonly IDeliveryService _service;
    public DeliveriesController(IDeliveryService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DeliveryCreateDto dto)
    {
        var result = await _service.AddDeliveryAsync(dto);
        return CreatedAtAction(null, result);
    }
}
