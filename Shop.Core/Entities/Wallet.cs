using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Shop.Core.Entities;

public class Wallet:BaseEntities
{
    public int Id { get; set; }
    public decimal Balance { get; set; } = 0;
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
    public ICollection<Card> Cards { get; set; }

}
