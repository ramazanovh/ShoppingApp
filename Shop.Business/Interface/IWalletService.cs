using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface IWalletService
{
    List<Wallet> GetAllWallets();
    void CreateWallet(Wallet newWallet, int userId);
    bool UpdateWallet(int walletId, int userId);
    bool DeleteWallet(int walletId);
    decimal GetWalletBalance(int userId);
    void IncreaseWalletBalance(int walletId, int cardId, decimal amount);
}
