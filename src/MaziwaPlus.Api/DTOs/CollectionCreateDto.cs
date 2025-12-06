namespace MaziwaPlus.Api.DTOs;

public class CollectionCreateDto
{
    public int FarmerId { get; set; }
    public DateTime CollectionDate { get; set; }
    public decimal LitersCollected { get; set; }
    public decimal RatePerLiter { get; set; }
}
