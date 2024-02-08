using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface IProductService
{
    List<Product> GetAllProducts();
    void CreateProduct(string name, string description, decimal price, int Quantity, int categoryId, int brandId, int discountId);
    void UpdateProduct(int productId, string newName, string newDescription, decimal newPrice, int Quantity, int newCategoryId, int newBrandId, int newDiscountId);
    void DeleteProduct(int productId);
    Task<bool> ActivateProduct(int productId);
    void DeactivateProduct(int productId);
}
