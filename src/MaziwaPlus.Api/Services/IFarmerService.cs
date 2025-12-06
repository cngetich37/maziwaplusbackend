using MaziwaPlus.Api.DTOs;

namespace MaziwaPlus.Api.Services;

public interface IFarmerService
{
    Task<FarmerSummaryDto> GetSummaryAsync(int farmerId);
}
