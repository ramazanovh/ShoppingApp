namespace Shop.Core.Entities;

public class ProductInvoices
{
    public int Id { get; set; }
    public int ProductCount { get; set; } = 0;
    public decimal ProductPrice { get; set; }  
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}
