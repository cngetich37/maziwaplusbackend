using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Domain.Entities;
using MaziwaPlus.Data.Repositories;

namespace MaziwaPlus.Api.Services;

public class FarmerService : IFarmerService
{
    private readonly IRepository<Farmer> _farmerRepo;
    private readonly IRepository<MilkCollection> _collectionRepo;

    public FarmerService(IRepository<Farmer> farmerRepo, IRepository<MilkCollection> collectionRepo)
    {
        _farmerRepo = farmerRepo;
        _collectionRepo = collectionRepo;
    }

    public async Task<FarmerSummaryDto> GetSummaryAsync(int farmerId)
    {
        var farmer = await _farmerRepo.GetByIdAsync(farmerId);
        if (farmer == null) throw new InvalidOperationException("Farmer not found");

        var collections = await _collectionRepo.ListAsync(c => c.FarmerId == farmerId);
        var total = collections.Sum(c => c.LitersCollected);

        return new FarmerSummaryDto
        {
            FarmerId = farmer.Id,
            FarmerName = farmer.Name,
            TotalLiters = total
        };
    }
}
