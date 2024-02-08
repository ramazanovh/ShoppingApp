namespace Shop.Core.Entities;

public class Invoice:BaseEntities
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; } = 0;
    public string? PaymentMethod { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public User User { get; set; }
    public int? WalletId { get; set; }
    public Wallet? Wallet { get; set; }
    public ICollection<ProductInvoices>? ProductInvoices { get; set; }
    public ICollection<Card> Cards { get;}
}
