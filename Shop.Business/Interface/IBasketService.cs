namespace Shop.Business.Interface;

public interface IBasketService
{
    void AddToBasket(int userId, int ProductId);
    void RemoveFromBasket(int basketId);
    void ClearBasket(int basketId);
}
