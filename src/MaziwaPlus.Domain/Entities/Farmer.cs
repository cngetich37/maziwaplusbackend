namespace MaziwaPlus.Domain.Entities;

public class Farmer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }

    public ICollection<MilkCollection> Collections { get; set; } = new List<MilkCollection>();
}
