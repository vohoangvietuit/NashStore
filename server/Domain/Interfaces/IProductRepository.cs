using Domain.Entities;

namespace Domain.Interfaces
{
  public interface IProductRepository
  {
    Task<Product> GetByIdAsync(int id);
    Task<List<Product>> ListAllAsync();
    Task<List<Product>> ListPaginatedAsync(int page, int pageSize, string? searchTerm, int? categoryId);
    Task<int> GetTotalCountAsync(string? searchTerm, int? categoryId);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<List<Product>> SearchProductsAsync(string searchTerm);
  }
}