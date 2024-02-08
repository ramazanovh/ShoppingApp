using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface ICategoryService
{
    void CreateCategory(string name);
    List<Category> GetAllCategories();
    void DeleteCategory(int categoryId);
    void UpdateCategory(int categoryId, string newName);
}
