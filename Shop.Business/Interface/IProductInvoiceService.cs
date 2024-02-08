using Shop.Business.Services;
using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface IProductInvoiceService
{
    bool CreateProductInvoice(ProductInvoices newInvoice);
}
