namespace Shop.Core.Entities;

public class Card:BaseEntities
{
    public Card()
    {
    }

    public Card(string? cardNumber, string? cardHolderName, int cvc, int userId, decimal balance, int walletId)
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        Cvc = cvc;
        UserId = userId;
        Balance = balance;
        WalletId = walletId;
    }

    public int Id { get; set; }
    public string? CardNumber { get; set; } = null!;
    public string? CardHolderName { get; set; } = null!;
    public int Cvc { get; set; }
    public decimal Balance { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; }
    public int invoiceId { get; set; }
    public Invoice Invoice { get; set; }
}
