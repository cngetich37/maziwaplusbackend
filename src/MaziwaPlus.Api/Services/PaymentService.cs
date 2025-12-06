using MaziwaPlus.Api.DTOs;
using MaziwaPlus.Domain.Entities;
using MaziwaPlus.Data.Repositories;

namespace MaziwaPlus.Api.Services;

public class PaymentService : IPaymentService
{
    private readonly IRepository<Payment> _paymentRepo;
    private readonly IRepository<Delivery> _deliveryRepo;

    public PaymentService(IRepository<Payment> paymentRepo, IRepository<Delivery> deliveryRepo)
    {
        _paymentRepo = paymentRepo;
        _deliveryRepo = deliveryRepo;
    }

    public async Task<PaymentDto> ProcessPaymentAsync(PaymentCreateDto dto)
    {
        if (dto.AmountPaid <= 0) throw new ArgumentException("Amount must be positive", nameof(dto.AmountPaid));

        var delivery = await _deliveryRepo.GetByIdAsync(dto.DeliveryId);
        if (delivery == null) throw new InvalidOperationException("Delivery not found");

        var payment = new Payment
        {
            DeliveryId = dto.DeliveryId,
            AmountPaid = dto.AmountPaid,
            PaymentDate = DateTime.UtcNow,
            PaymentStatus = PaymentStatus.Completed
        };

        var added = await _paymentRepo.AddAsync(payment);

        return new PaymentDto
        {
            Id = added.Id,
            DeliveryId = added.DeliveryId,
            AmountPaid = added.AmountPaid,
            PaymentDate = added.PaymentDate,
            Status = added.PaymentStatus.ToString()
        };
    }
}
