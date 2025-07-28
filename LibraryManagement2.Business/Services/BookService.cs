using AutoMapper;
using LibraryManagement2.Business.Interfaces;
using LibraryManagement2.Data.Entities;
using LibraryManagement2.Data.Repositories.Interfaces;
using LibraryManagement2.Shared.DTOs;

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

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task AddAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
