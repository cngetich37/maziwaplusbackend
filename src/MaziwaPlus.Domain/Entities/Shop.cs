namespace MaziwaPlus.Domain.Entities;

public class Shop
{
    public int Id { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string? Location { get; set; }

    public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
