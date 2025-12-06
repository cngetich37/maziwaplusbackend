using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Domain.Entities;
using MaziwaPlus.Data.Repositories;

namespace MaziwaPlus.Api.Services;

public class CollectionService : ICollectionService
{
    private readonly IRepository<MilkCollection> _collectionRepo;
    private readonly IRepository<Farmer> _farmerRepo;

    public CollectionService(IRepository<MilkCollection> collectionRepo, IRepository<Farmer> farmerRepo)
    {
        _collectionRepo = collectionRepo;
        _farmerRepo = farmerRepo;
    }

    public async Task<CollectionDto> AddCollectionAsync(CollectionCreateDto dto)
    {
        if (dto.LitersCollected <= 0) throw new ArgumentException("Liters must be positive", nameof(dto.LitersCollected));

        var farmer = await _farmerRepo.GetByIdAsync(dto.FarmerId);
        if (farmer == null) throw new InvalidOperationException("Farmer not found");

        var entity = new MilkCollection
        {
            FarmerId = dto.FarmerId,
            CollectionDate = dto.CollectionDate,
            LitersCollected = dto.LitersCollected,
            RatePerLiter = dto.RatePerLiter
        };

        var added = await _collectionRepo.AddAsync(entity);

        return new CollectionDto
        {
            Id = added.Id,
            FarmerId = added.FarmerId,
            CollectionDate = added.CollectionDate,
            LitersCollected = added.LitersCollected,
            RatePerLiter = added.RatePerLiter,
            TotalCost = added.TotalCost
        };
    }

    public async Task<decimal> GetDailyTotalAsync(DateTime date)
    {
        var list = await _collectionRepo.ListAsync(c => c.CollectionDate.Date == date.Date);
        return list.Sum(c => c.LitersCollected);
    }
}
