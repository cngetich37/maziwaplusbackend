using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Domain.Entities;
using MaziwaPlus.Data.Repositories;

namespace MaziwaPlus.Api.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IRepository<Delivery> _deliveryRepo;
    private readonly IRepository<Shop> _shopRepo;

    public DeliveryService(IRepository<Delivery> deliveryRepo, IRepository<Shop> shopRepo)
    {
        _deliveryRepo = deliveryRepo;
        _shopRepo = shopRepo;
    }

    public async Task<DeliveryDto> AddDeliveryAsync(DeliveryCreateDto dto)
    {
        if (dto.LitersDelivered <= 0) throw new ArgumentException("Liters must be positive", nameof(dto.LitersDelivered));

        var shop = await _shopRepo.GetByIdAsync(dto.ShopId);
        if (shop == null) throw new InvalidOperationException("Shop not found");

        var entity = new Delivery
        {
            ShopId = dto.ShopId,
            DeliveryDate = dto.DeliveryDate,
            LitersDelivered = dto.LitersDelivered,
            Status = DeliveryStatus.Pending
        };

        var added = await _deliveryRepo.AddAsync(entity);

        return new DeliveryDto
        {
            Id = added.Id,
            ShopId = added.ShopId,
            DeliveryDate = added.DeliveryDate,
            LitersDelivered = added.LitersDelivered,
            Status = added.Status.ToString()
        };
    }
}
