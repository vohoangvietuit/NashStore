using Domain.Entities;

namespace Application.Interfaces;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(int id);
    Task<List<Product>> GetAllProductsAsync();
    Task<(List<Product>, int)> GetPaginatedProductsAsync(int page, int pageSize, string? searchTerm, int? categoryId);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<List<Product>> SearchProductsAsync(string searchTerm);
}
