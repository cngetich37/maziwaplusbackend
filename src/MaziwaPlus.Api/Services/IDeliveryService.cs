using MaziwaPlus.Api.DTOs;

namespace MaziwaPlus.Api.Services;

public interface IDeliveryService
{
    Task<DeliveryDto> AddDeliveryAsync(DeliveryCreateDto dto);
}
