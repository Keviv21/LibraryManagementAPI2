using LibraryManagement2.Business.Interfaces;
using LibraryManagement2.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement2.API.Controllers
{
    /// <summary>
    /// The BookController handles all operations related to book records in the library system.
    /// It supports retrieval, creation, update, and deletion of books. Authorization is enforced based on user roles.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Retrieves a list of all books available in the system.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        /// <summary>
        /// Retrieves a specific book by its ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Creates a new book record. Only accessible by Admins.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bookService.AddAsync(bookDto);
            return StatusCode(201);
        }

        /// <summary>
        /// Updates an existing book by ID. Only Admins can perform this action.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (id != bookDto.Id)
                return BadRequest("Book ID mismatch");

            var existingBook = await _bookService.GetByIdAsync(id);
            if (existingBook == null)
                return NotFound();

            await _bookService.UpdateAsync(bookDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a book record by ID. Only Admins can delete books.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
