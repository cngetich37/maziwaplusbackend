using Microsoft.EntityFrameworkCore;
using MaziwaPlus.Data.Data;
using MaziwaPlus.Data.Repositories;
using MaziwaPlus.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext - connection string placeholder in appsettings.json
builder.Services.AddDbContext<MaziwaPlusContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("MaziwaPlus.Api")));

// Repositories & Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IFarmerService, FarmerService>();

var app = builder.Build();

// Always enable Swagger and seed data for development/testing
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MaziwaPlus API v1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at root
});

// Seed the database in development
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<MaziwaPlusContext>();
        SeedData.Seed(context);
    }
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();
