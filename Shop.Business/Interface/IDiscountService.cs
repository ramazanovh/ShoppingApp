using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface IDiscountService
{
    void CreateDiscount(string name, string description, decimal discountPercentage, DateTime startDate, DateTime endDate);
    List<Discount> GetAllDiscounts();
    void UpdateDiscountAsync(int discountId, string newName, string newDescription, decimal newDiscountPresent, DateTime newStartTime, DateTime newEndTime);
    void DeleteDiscountAsync(int discountId);
}
