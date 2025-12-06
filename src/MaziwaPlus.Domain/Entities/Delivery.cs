namespace MaziwaPlus.Domain.Entities;

public enum DeliveryStatus { Pending, Accepted, Rejected }

public class Delivery
{
    public int Id { get; set; }
    public int ShopId { get; set; }
    public Shop Shop { get; set; } = null!;
    public DateTime DeliveryDate { get; set; }
    public decimal LitersDelivered { get; set; }
    public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
