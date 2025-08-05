using AutoMapper;
using LibraryManagement2.Business.Interfaces;
using LibraryManagement2.Data.Entities;
using LibraryManagement2.Data.Repositories.Interfaces;
using LibraryManagement2.Shared.DTO.MainData;
using LibraryManagement2.Shared.Pagination;
using LibraryManagement2.Shared.Response;

namespace LibraryManagement2.Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ServiceOperationResult<IEnumerable<BookDto>>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return ServiceOperationResult<IEnumerable<BookDto>>.SuccessResult(bookDtos,"List of all the books in this library");
        }

        public async Task<ServiceOperationResult<BookDto>> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
                return ServiceOperationResult<BookDto>.FailureResult("Book not found.");

            var bookDto = _mapper.Map<BookDto>(book);
            return ServiceOperationResult<BookDto>.SuccessResult(bookDto,$"Book with id = {id} ");
        }

        public async Task<ServiceOperationResult<BookDto>> AddAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.AddAsync(book);
            var resultDto = _mapper.Map<BookDto>(book);
            return ServiceOperationResult<BookDto>.SuccessResult(resultDto,"Book added successfully.");
        }

        public async Task<ServiceOperationResult<BookDto>> UpdateAsync(BookDto bookDto)
        {
            var existingBook = await _bookRepository.GetByIdAsync(bookDto.Id);

            if (existingBook == null)
                return ServiceOperationResult<BookDto>.FailureResult("Book not found.");

            var updatedBook = _mapper.Map<Book>(bookDto);
            await _bookRepository.UpdateAsync(updatedBook);

            return ServiceOperationResult<BookDto>.SuccessResult(bookDto,"Book updated successfully.");
        }

        public async Task<ServiceOperationResult<string?>> DeleteAsync(int id)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);

            if (existingBook == null)
                return ServiceOperationResult<string?>.FailureResult("Book not found.");

            await _bookRepository.DeleteAsync(id);
            return ServiceOperationResult<string?>.SuccessResult(null,"Book deleted successfully.");
        }

        public async Task<ServiceOperationResult<PaginatedResult<BookDto>>> GetPaginatedBooksAsync(PaginationParams paginationParams)
        {
            var result = await _bookRepository.GetPaginatedBooksAsync(paginationParams);

            var mappedItems = _mapper.Map<List<BookDto>>(result.Items);

            var mappedResult = new PaginatedResult<BookDto>(
                mappedItems,
                result.TotalCount,
                result.PageNumber,
                result.PageSize
            );

            return ServiceOperationResult<PaginatedResult<BookDto>>.SuccessResult(mappedResult,"Books shown in pages");
        }
    }
}
