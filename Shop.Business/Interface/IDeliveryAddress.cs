using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface IDeliveryAddress
{
    void CreateDeliveryAddress(string address, string postalCode, int userId);
    List<DeliveryAddress> GetAllDeliveryAddresses(int userId);
    void GetDeliveryAddressById(int deliveryAddressId);
    void UpdateDeliveryAddress(int deliveryAddressId, string newAddress, string newPostalCode);
    void DeleteDeliveryAddress(int deliveryAddressId);
}
