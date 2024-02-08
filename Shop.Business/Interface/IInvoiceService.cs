using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface IInvoiceService
{
    void CreateInvoice(Invoice invoice, int cardId, int userId);
}
