using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MaziwaPlus.Api.Services;
using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Data.Repositories;
using MaziwaPlus.Domain.Entities;

namespace MaziwaPlus.Tests;

public class CollectionServiceTests
{
    [Fact]
    public async Task AddCollectionAsync_ValidInput_AddsAndReturnsDto()
    {
        var mockCollectionRepo = new Mock<IRepository<MilkCollection>>();
        var mockFarmerRepo = new Mock<IRepository<Farmer>>();

        mockFarmerRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Farmer { Id = 1, Name = "Test Farmer" });
        mockCollectionRepo.Setup(r => r.AddAsync(It.IsAny<MilkCollection>())).ReturnsAsync((MilkCollection m) => { m.Id = 5; return m; });

        var service = new CollectionService(mockCollectionRepo.Object, mockFarmerRepo.Object);

        var dto = new CollectionCreateDto { FarmerId = 1, CollectionDate = DateTime.UtcNow.Date, LitersCollected = 10m, RatePerLiter = 2m };
        var result = await service.AddCollectionAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(5, result.Id);
        Assert.Equal(10m, result.LitersCollected);
        Assert.Equal(20m, result.TotalCost);
    }

    [Fact]
    public async Task GetDailyTotalAsync_ReturnsSum()
    {
        var mockCollectionRepo = new Mock<IRepository<MilkCollection>>();
        var mockFarmerRepo = new Mock<IRepository<Farmer>>();

        var date = DateTime.UtcNow.Date;
        mockCollectionRepo.Setup(r => r.ListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MilkCollection, bool>>>() )).ReturnsAsync(new System.Collections.Generic.List<MilkCollection>
        {
            new MilkCollection { LitersCollected = 5m, CollectionDate = date },
            new MilkCollection { LitersCollected = 3m, CollectionDate = date }
        });

        var service = new CollectionService(mockCollectionRepo.Object, mockFarmerRepo.Object);
        var total = await service.GetDailyTotalAsync(date);

        Assert.Equal(8m, total);
    }
}
