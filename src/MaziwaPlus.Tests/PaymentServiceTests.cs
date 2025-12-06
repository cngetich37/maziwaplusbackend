using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MaziwaPlus.Api.Services;
using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Data.Repositories;
using MaziwaPlus.Domain.Entities;

namespace MaziwaPlus.Tests;

public class PaymentServiceTests
{
    [Fact]
    public async Task ProcessPaymentAsync_ValidInput_AddsAndReturnsDto()
    {
        var mockPaymentRepo = new Mock<IRepository<Payment>>();
        var mockDeliveryRepo = new Mock<IRepository<Delivery>>();

        mockDeliveryRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Delivery { Id = 1, ShopId = 1, LitersDelivered = 50m });
        mockPaymentRepo.Setup(r => r.AddAsync(It.IsAny<Payment>())).ReturnsAsync((Payment p) => { p.Id = 10; return p; });

        var service = new PaymentService(mockPaymentRepo.Object, mockDeliveryRepo.Object);

        var dto = new PaymentCreateDto { DeliveryId = 1, AmountPaid = 1000m };
        var result = await service.ProcessPaymentAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(10, result.Id);
        Assert.Equal(1000m, result.AmountPaid);
        Assert.Equal("Completed", result.Status);
    }

    [Fact]
    public async Task ProcessPaymentAsync_InvalidAmount_ThrowsException()
    {
        var mockPaymentRepo = new Mock<IRepository<Payment>>();
        var mockDeliveryRepo = new Mock<IRepository<Delivery>>();

        var service = new PaymentService(mockPaymentRepo.Object, mockDeliveryRepo.Object);

        var dto = new PaymentCreateDto { DeliveryId = 1, AmountPaid = -100m };

        await Assert.ThrowsAsync<ArgumentException>(() => service.ProcessPaymentAsync(dto));
    }

    [Fact]
    public async Task ProcessPaymentAsync_DeliveryNotFound_ThrowsException()
    {
        var mockPaymentRepo = new Mock<IRepository<Payment>>();
        var mockDeliveryRepo = new Mock<IRepository<Delivery>>();

        mockDeliveryRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Delivery)null);

        var service = new PaymentService(mockPaymentRepo.Object, mockDeliveryRepo.Object);

        var dto = new PaymentCreateDto { DeliveryId = 999, AmountPaid = 1000m };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.ProcessPaymentAsync(dto));
    }
}
