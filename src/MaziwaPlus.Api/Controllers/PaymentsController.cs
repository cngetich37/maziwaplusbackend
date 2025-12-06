using Microsoft.AspNetCore.Mvc;
using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Api.Services;

namespace MaziwaPlus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _service;
    public PaymentsController(IPaymentService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PaymentCreateDto dto)
    {
        var result = await _service.ProcessPaymentAsync(dto);
        return CreatedAtAction(null, result);
    }
}
