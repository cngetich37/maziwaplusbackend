namespace MaziwaPlus.Api.DTOs;

public class PaymentDto
{
    public int Id { get; set; }
    public int DeliveryId { get; set; }
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
