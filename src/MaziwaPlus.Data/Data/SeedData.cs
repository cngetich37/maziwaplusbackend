using MaziwaPlus.Domain.Entities;

namespace MaziwaPlus.Data.Data;

public static class SeedData
{
    public static void Seed(MaziwaPlusContext context)
    {
        // Skip if data already exists
        if (context.Farmers.Any() || context.Shops.Any())
        {
            return;
        }

        // Seed Farmers
        var farmers = new[]
        {
            new Farmer { Name = "John Doe", PhoneNumber = "+254712345678", Address = "Kisumu" },
            new Farmer { Name = "Jane Smith", PhoneNumber = "+254723456789", Address = "Nakuru" },
            new Farmer { Name = "Peter Kipchoge", PhoneNumber = "+254734567890", Address = "Eldoret" }
        };

        context.Farmers.AddRange(farmers);
        context.SaveChanges();

        // Seed Shops
        var shops = new[]
        {
            new Shop { ShopName = "Main Dairy Store", Location = "Nairobi CBD" },
            new Shop { ShopName = "Healthy Milk Mart", Location = "Westlands" },
            new Shop { ShopName = "Fresh Milk Kiosk", Location = "Karen" }
        };

        context.Shops.AddRange(shops);
        context.SaveChanges();

        // Seed Milk Collections
        var collections = new[]
        {
            new MilkCollection { FarmerId = farmers[0].Id, CollectionDate = DateTime.UtcNow.AddDays(-2), LitersCollected = 50m, RatePerLiter = 2.50m },
            new MilkCollection { FarmerId = farmers[0].Id, CollectionDate = DateTime.UtcNow.AddDays(-1), LitersCollected = 60m, RatePerLiter = 2.50m },
            new MilkCollection { FarmerId = farmers[1].Id, CollectionDate = DateTime.UtcNow.AddDays(-2), LitersCollected = 40m, RatePerLiter = 2.50m },
            new MilkCollection { FarmerId = farmers[2].Id, CollectionDate = DateTime.UtcNow.AddDays(-1), LitersCollected = 70m, RatePerLiter = 2.50m }
        };

        context.MilkCollections.AddRange(collections);
        context.SaveChanges();

        // Seed Deliveries
        var deliveries = new[]
        {
            new Delivery { ShopId = shops[0].Id, DeliveryDate = DateTime.UtcNow.AddDays(-1), LitersDelivered = 100m, Status = DeliveryStatus.Accepted },
            new Delivery { ShopId = shops[1].Id, DeliveryDate = DateTime.UtcNow.AddDays(-1), LitersDelivered = 80m, Status = DeliveryStatus.Accepted },
            new Delivery { ShopId = shops[2].Id, DeliveryDate = DateTime.UtcNow, LitersDelivered = 50m, Status = DeliveryStatus.Pending }
        };

        context.Deliveries.AddRange(deliveries);
        context.SaveChanges();

        // Seed Payments
        var payments = new[]
        {
            new Payment { DeliveryId = deliveries[0].Id, AmountPaid = 250m, PaymentDate = DateTime.UtcNow.AddDays(-1), PaymentStatus = PaymentStatus.Completed },
            new Payment { DeliveryId = deliveries[1].Id, AmountPaid = 200m, PaymentDate = DateTime.UtcNow.AddDays(-1), PaymentStatus = PaymentStatus.Completed }
        };

        context.Payments.AddRange(payments);
        context.SaveChanges();
    }
}
