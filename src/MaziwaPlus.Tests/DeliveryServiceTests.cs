using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MaziwaPlus.Api.Services;
using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Data.Repositories;
using MaziwaPlus.Domain.Entities;

namespace MaziwaPlus.Tests;

public class DeliveryServiceTests
{
    [Fact]
    public async Task AddDeliveryAsync_ValidInput_AddsAndReturnsDto()
    {
        var mockDeliveryRepo = new Mock<IRepository<Delivery>>();
        var mockShopRepo = new Mock<IRepository<Shop>>();

        mockShopRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Shop { Id = 1, ShopName = "Test Shop" });
        mockDeliveryRepo.Setup(r => r.AddAsync(It.IsAny<Delivery>())).ReturnsAsync((Delivery d) => { d.Id = 5; return d; });

        var service = new DeliveryService(mockDeliveryRepo.Object, mockShopRepo.Object);

        var dto = new DeliveryCreateDto { ShopId = 1, DeliveryDate = DateTime.UtcNow.Date, LitersDelivered = 50m };
        var result = await service.AddDeliveryAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(5, result.Id);
        Assert.Equal(50m, result.LitersDelivered);
        Assert.Equal("Pending", result.Status);
    }

    [Fact]
    public async Task AddDeliveryAsync_InvalidLiters_ThrowsException()
    {
        var mockDeliveryRepo = new Mock<IRepository<Delivery>>();
        var mockShopRepo = new Mock<IRepository<Shop>>();

        var service = new DeliveryService(mockDeliveryRepo.Object, mockShopRepo.Object);

        var dto = new DeliveryCreateDto { ShopId = 1, DeliveryDate = DateTime.UtcNow.Date, LitersDelivered = -5m };

        await Assert.ThrowsAsync<ArgumentException>(() => service.AddDeliveryAsync(dto));
    }

    [Fact]
    public async Task AddDeliveryAsync_ShopNotFound_ThrowsException()
    {
        var mockDeliveryRepo = new Mock<IRepository<Delivery>>();
        var mockShopRepo = new Mock<IRepository<Shop>>();

        mockShopRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Shop)null);

        var service = new DeliveryService(mockDeliveryRepo.Object, mockShopRepo.Object);

        var dto = new DeliveryCreateDto { ShopId = 999, DeliveryDate = DateTime.UtcNow.Date, LitersDelivered = 50m };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddDeliveryAsync(dto));
    }
}
