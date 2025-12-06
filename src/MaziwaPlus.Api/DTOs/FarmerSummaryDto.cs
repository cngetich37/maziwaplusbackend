namespace MaziwaPlus.Api.DTOs;

public class FarmerSummaryDto
{
    public int FarmerId { get; set; }
    public string FarmerName { get; set; } = string.Empty;
    public decimal TotalLiters { get; set; }
}
