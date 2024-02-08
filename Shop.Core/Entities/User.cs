namespace Shop.Core.Entities;

public class User : BaseEntities
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string UserName { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } 
    public string Password { get; set; } = null!;
    public bool isAdmin { get; set; } = false;
    public Basket Basket { get; set; }
    public ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
    public ICollection<Wallet>? Wallets { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
}

