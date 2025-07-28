using LibraryManagement2.Data.DbContext;
using LibraryManagement2.Data.Entities;
using LibraryManagement2.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement2.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
