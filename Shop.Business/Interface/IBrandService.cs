using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface IBrandService
{
    Task<Brand> CreateBrandAsync(string name);
    void UptadeBrand(int brandId, string newName);
    void DelateBrandAsync(int brandId, string name);
    List<Brand> GetAllBrandsAsync();
}
