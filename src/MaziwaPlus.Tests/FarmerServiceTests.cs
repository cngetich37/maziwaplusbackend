using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MaziwaPlus.Api.Services;
using MaziwaPlus.Data.Repositories;
using MaziwaPlus.Domain.Entities;

namespace MaziwaPlus.Tests;

public class FarmerServiceTests
{
    [Fact]
    public async Task GetSummaryAsync_ValidFarmer_ReturnsCorrectSummary()
    {
        var mockFarmerRepo = new Mock<IRepository<Farmer>>();
        var mockCollectionRepo = new Mock<IRepository<MilkCollection>>();

        var farmer = new Farmer { Id = 1, Name = "John Doe" };
        mockFarmerRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(farmer);

        var collections = new System.Collections.Generic.List<MilkCollection>
        {
            new MilkCollection { Id = 1, FarmerId = 1, LitersCollected = 20m, CollectionDate = DateTime.UtcNow.Date },
            new MilkCollection { Id = 2, FarmerId = 1, LitersCollected = 30m, CollectionDate = DateTime.UtcNow.Date }
        };
        mockCollectionRepo.Setup(r => r.ListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MilkCollection, bool>>>())).ReturnsAsync(collections);

        var service = new FarmerService(mockFarmerRepo.Object, mockCollectionRepo.Object);

        var result = await service.GetSummaryAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.FarmerId);
        Assert.Equal("John Doe", result.FarmerName);
        Assert.Equal(50m, result.TotalLiters);
    }

    [Fact]
    public async Task GetSummaryAsync_FarmerNotFound_ThrowsException()
    {
        var mockFarmerRepo = new Mock<IRepository<Farmer>>();
        var mockCollectionRepo = new Mock<IRepository<MilkCollection>>();

        mockFarmerRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Farmer)null);

        var service = new FarmerService(mockFarmerRepo.Object, mockCollectionRepo.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.GetSummaryAsync(999));
    }

    [Fact]
    public async Task GetSummaryAsync_NoCollections_ReturnsSummaryWithZeroLiters()
    {
        var mockFarmerRepo = new Mock<IRepository<Farmer>>();
        var mockCollectionRepo = new Mock<IRepository<MilkCollection>>();

        var farmer = new Farmer { Id = 2, Name = "Jane Smith" };
        mockFarmerRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(farmer);

        var collections = new System.Collections.Generic.List<MilkCollection>();
        mockCollectionRepo.Setup(r => r.ListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MilkCollection, bool>>>())).ReturnsAsync(collections);

        var service = new FarmerService(mockFarmerRepo.Object, mockCollectionRepo.Object);

        var result = await service.GetSummaryAsync(2);

        Assert.NotNull(result);
        Assert.Equal(2, result.FarmerId);
        Assert.Equal("Jane Smith", result.FarmerName);
        Assert.Equal(0m, result.TotalLiters);
    }
}
