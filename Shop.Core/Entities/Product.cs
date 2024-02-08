namespace Shop.Core.Entities;

public class Product: BaseEntities
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public decimal Price { get; set; } = 0;
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public int DiscountId { get; set; }
    public Discount Discount { get; set; }
    public int BasketId { get; set; }
    public Basket Basket { get; set; }
    public ICollection<ProductInvoices>? ProductInvoices { get; set; }
}
