using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
  public class OrderRepository : IOrderRepository
  {
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
      return await _dbContext.Orders
          .Include(o => o.OrderDetails)
          .ThenInclude(od => od.Product)
          .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Order>> ListAllAsync()
    {
      return await _dbContext.Orders
          .Include(o => o.OrderDetails)
          .ThenInclude(od => od.Product)
          .ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
      _dbContext.Orders.Add(order);
      await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
      _dbContext.Entry(order).State = EntityState.Modified;
      await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      var order = await _dbContext.Orders.FindAsync(id);
      if (order != null)
      {
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
      }
    }
  }
}

