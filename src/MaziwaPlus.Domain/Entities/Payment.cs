namespace MaziwaPlus.Domain.Entities;

public enum PaymentStatus { Pending, Completed, Failed }

public class Payment
{
    public int Id { get; set; }
    public int DeliveryId { get; set; }
    public Delivery Delivery { get; set; } = null!;
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
}
