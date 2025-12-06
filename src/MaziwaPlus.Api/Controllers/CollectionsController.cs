using Microsoft.AspNetCore.Mvc;
using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Api.Services;

namespace MaziwaPlus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionService _service;
    public CollectionsController(ICollectionService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CollectionCreateDto dto)
    {
        var result = await _service.AddCollectionAsync(dto);
        return CreatedAtAction(nameof(GetDailyTotal), new { date = result.CollectionDate.ToString("yyyy-MM-dd") }, result);
    }

    [HttpGet("daily")]
    public async Task<IActionResult> GetDailyTotal([FromQuery] DateTime? date)
    {
        var dt = date ?? DateTime.UtcNow.Date;
        var total = await _service.GetDailyTotalAsync(dt);
        return Ok(new { date = dt.Date, totalLiters = total });
    }
}
