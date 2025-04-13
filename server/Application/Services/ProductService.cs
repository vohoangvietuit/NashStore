using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.ListAllAsync();
    }
     public async Task<(List<Product>, int)> GetPaginatedProductsAsync(int page, int pageSize, string? searchTerm, int? categoryId)
    {
        var products = await _productRepository.ListPaginatedAsync(page, pageSize, searchTerm, categoryId);
        var totalCount = await _productRepository.GetTotalCountAsync(searchTerm, categoryId);
        return (products, totalCount);
    }

    public async Task CreateProductAsync(Product product)
    {
        product.Date = DateTime.UtcNow; // Set the date
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task<List<Product>> SearchProductsAsync(string searchTerm)
    {
        return await _productRepository.SearchProductsAsync(searchTerm);
    }
}