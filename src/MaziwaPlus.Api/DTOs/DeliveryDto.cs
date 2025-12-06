namespace MaziwaPlus.Api.DTOs;

public class DeliveryDto
{
    public int Id { get; set; }
    public int ShopId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public decimal LitersDelivered { get; set; }
    public string Status { get; set; } = string.Empty;
}
