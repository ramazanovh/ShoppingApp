namespace Shop.Core.Entities;

public class Basket:BaseEntities
{
    public int Id { get; set; }
    public int Quantity { get; set; } = 0;  
    public User User { get; set; }
    public int UserId { get; set; }
    public ICollection<Product>? Products { get; set; }

}
