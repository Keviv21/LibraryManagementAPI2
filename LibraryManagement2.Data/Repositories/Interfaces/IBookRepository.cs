using LibraryManagement2.Data.Entities;
using LibraryManagement2.Shared.Pagination;

namespace LibraryManagement2.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<PaginatedResult<Book>> GetPaginatedBooksAsync(PaginationParams paginationParams);

    }
}
