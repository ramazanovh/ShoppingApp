namespace Shop.Core.Entities;

public class Discount
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public decimal DiscountPrecent { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ICollection<Product>? Products { get; set; }
}
