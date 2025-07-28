using LibraryManagement2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement2.Data.DbContext
{
    public class LibraryDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
