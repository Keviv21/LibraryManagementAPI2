using LibraryManagement2.Shared.DTOs;

namespace LibraryManagement2.Business.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task AddAsync(BookDto bookDto);
        Task UpdateAsync(BookDto bookDto);
        Task DeleteAsync(int id);
    }
}
