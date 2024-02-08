using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface ICardService
{
    List<Card> GetAllCards();
    void CreateCard(int userId, string cardNumber, string cardHolderName, int cvc);
    void UpdateCard(int cardId, string cardNumber, string cardHolderName, int cvc);
    void DeleteCard(int cardId);
    Task<decimal >GetCardBalanceAsync(int cardId);
    Task<bool> CardExists(int cardId);
}
