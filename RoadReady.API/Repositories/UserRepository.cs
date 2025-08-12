using Microsoft.EntityFrameworkCore;
using RoadReady.API.Interfaces;
using RoadReady.API.Models;
using RoadReady.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadReady.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RoadReadyDbContext _context;

        public UserRepository(RoadReadyDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
