using LibraryManagement2.Data.DbContext;
using LibraryManagement2.Data.Entities;
using LibraryManagement2.Data.Repositories.Interfaces;
using LibraryManagement2.Shared.Pagination;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement2.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);

            if (existingBook == null)
            {
                throw new ArgumentException($"Book with ID {book.Id} not found.");
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.Category = book.Category;
            existingBook.IsAvailable = book.IsAvailable;
            existingBook.PublishedDate = book.PublishedDate;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<PaginatedResult<Book>> GetPaginatedBooksAsync(PaginationParams paginationParams)
        {
            paginationParams.Normalize();

            var query = _context.Books.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PaginatedResult<Book>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
        }

    }
}
