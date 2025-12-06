namespace MaziwaPlus.Api.DTOs;

public class CollectionDto
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public DateTime CollectionDate { get; set; }
    public decimal LitersCollected { get; set; }
    public decimal RatePerLiter { get; set; }
    public decimal TotalCost { get; set; }
}
