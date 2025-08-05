using LibraryManagement2.Shared.DTO.MainData;
using LibraryManagement2.Shared.Pagination;
using LibraryManagement2.Shared.Response;

namespace LibraryManagement2.Business.Interfaces
{
    public interface IBookService
    {
        Task<ServiceOperationResult<IEnumerable<BookDto>>> GetAllAsync();
        Task<ServiceOperationResult<BookDto>> GetByIdAsync(int id);
        Task<ServiceOperationResult<BookDto>> AddAsync(BookDto bookDto);
        Task<ServiceOperationResult<BookDto>> UpdateAsync(BookDto bookDto);
        Task<ServiceOperationResult<string?>> DeleteAsync(int id);
        Task<ServiceOperationResult<PaginatedResult<BookDto>>> GetPaginatedBooksAsync(PaginationParams paginationParam);
    }
}
