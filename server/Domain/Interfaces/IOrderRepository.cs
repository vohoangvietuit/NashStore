using Domain.Entities;

namespace Domain.Interfaces
{
public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task<List<Order>> ListAllAsync();
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);
}
}