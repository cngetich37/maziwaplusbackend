namespace MaziwaPlus.Api.DTOs;

public class DeliveryCreateDto
{
    public int ShopId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public decimal LitersDelivered { get; set; }
}
