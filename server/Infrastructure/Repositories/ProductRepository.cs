using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
      return await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> ListAllAsync()
    {
      return await _dbContext.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<List<Product>> ListPaginatedAsync(int page, int pageSize, string? searchTerm, int? categoryId)
    {
      IQueryable<Product> query = _dbContext.Products.Include(p => p.Category);

      if (!string.IsNullOrEmpty(searchTerm))
      {
        query = query.Where(p => p.Name.Contains(searchTerm));
      }

      if (categoryId.HasValue)
      {
        query = query.Where(p => p.CategoryId == categoryId);
      }

      return await query
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? searchTerm, int? categoryId)
    {
      IQueryable<Product> query = _dbContext.Products;

      if (!string.IsNullOrEmpty(searchTerm))
      {
        query = query.Where(p => p.Name.Contains(searchTerm));
      }

      if (categoryId.HasValue)
      {
        query = query.Where(p => p.CategoryId == categoryId);
      }

      return await query.CountAsync();
    }

    public async Task AddAsync(Product product)
    {
      _dbContext.Products.Add(product);
      await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
      _dbContext.Entry(product).State = EntityState.Modified;
      await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      var product = await _dbContext.Products.FindAsync(id);
      if (product != null)
      {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
      }
    }

    public async Task<List<Product>> SearchProductsAsync(string searchTerm)
    {
      return await _dbContext.Products
          .Where(p => p.Name.Contains(searchTerm))
          .ToListAsync();
    }
  }
}

