using MaziwaPlus.Api.DTOs;

namespace MaziwaPlus.Api.Services;

public interface ICollectionService
{
    Task<CollectionDto> AddCollectionAsync(CollectionCreateDto dto);
    Task<decimal> GetDailyTotalAsync(DateTime date);
}
