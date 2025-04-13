using Domain.Entities;

namespace Domain.Interfaces
{
public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(int id);
    Task<List<Category>> ListAllAsync();
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
}
}