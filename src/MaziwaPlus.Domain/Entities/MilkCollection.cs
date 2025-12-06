using System.ComponentModel.DataAnnotations.Schema;

namespace MaziwaPlus.Domain.Entities;

public class MilkCollection
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public Farmer Farmer { get; set; } = null!;
    public DateTime CollectionDate { get; set; }
    public decimal LitersCollected { get; set; }
    public decimal RatePerLiter { get; set; }
    [NotMapped]
    public decimal TotalCost => LitersCollected * RatePerLiter;
}
