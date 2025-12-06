using MaziwaPlus.Api.DTOs;

namespace MaziwaPlus.Api.Services;

public interface IPaymentService
{
    Task<PaymentDto> ProcessPaymentAsync(PaymentCreateDto dto);
}
