using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsername(string username);
    Task<User?> GetUserById(int id);
    Task<List<User>> GetAllUsers();
    Task<bool> AddUser(User user);
    Task<bool> UpdateUser(User user);
    Task<bool> DeleteUser(int id);
}
