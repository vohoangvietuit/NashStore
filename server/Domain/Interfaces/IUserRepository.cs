using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsername(string username);
    Task<bool> AddUser(User user);
}
