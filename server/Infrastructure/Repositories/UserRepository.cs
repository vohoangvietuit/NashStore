using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsername(string username)
            => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<User?> GetUserById(int id)
            => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<List<User>> GetAllUsers()
            => await _context.Users.ToListAsync();

        public async Task<bool> AddUser(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            if (user == null) return false;
            
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

