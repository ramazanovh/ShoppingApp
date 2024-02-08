using Shop.Core.Entities;

namespace Shop.Business.Interface;

public interface IUserService
{
    void CreateUser(User user, string name, string surname, string username, string UserEmail, string UserPassword, string UserPhone, bool isAdmin);
    void DeleteUser(string userEmail);
    void ActivateProfile(int UserId);
    void DeactivateProfile(int UserId);
    void Login(string username, string UserPassword);
    Task<bool> Register(string username, string email, string password);
    Task<bool> IsUserAdmin(string userName);
    void UpdateProfile(string name, string newUsername, string newUserEmail, string newUserPassword, string newUserPhone);
    List <User> GetAllUsers();
}
