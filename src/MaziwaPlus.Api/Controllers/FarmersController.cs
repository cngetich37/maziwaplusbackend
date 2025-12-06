using Microsoft.AspNetCore.Mvc;
using MaziwaPlus.Api.Services;

namespace MaziwaPlus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FarmersController : ControllerBase
{
    private readonly IFarmerService _service;
    public FarmersController(IFarmerService service) => _service = service;

    [HttpGet("{id}/summary")]
    public async Task<IActionResult> Summary(int id)
    {
        var result = await _service.GetSummaryAsync(id);
        return Ok(result);
    }
}
