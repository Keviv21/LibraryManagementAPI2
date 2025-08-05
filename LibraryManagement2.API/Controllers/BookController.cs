using LibraryManagement2.Business.Interfaces;
using LibraryManagement2.Shared.DTO.MainData;
using LibraryManagement2.Shared.Pagination;
using LibraryManagement2.Shared.Response;
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
        [ProducesResponseType(typeof(ServiceOperationResult<IEnumerable<BookDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific book by its ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceOperationResult<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(int id)
        {
            var result = await _bookService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new book record. Only accessible by Admins.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ServiceOperationResult<BookDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookService.AddAsync(bookDto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return StatusCode(201, result);
        }

        /// <summary>
        /// Updates an existing book by ID. Only Admins can perform this action.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ServiceOperationResult<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (id != bookDto.Id)
                return BadRequest("Book ID mismatch");

            var result = await _bookService.UpdateAsync(bookDto);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Deletes a book record by ID. Only Admins can delete books.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ServiceOperationResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a paginated list of books.
        /// </summary>
        [HttpGet("paginated")]
        [ProducesResponseType(typeof(ServiceOperationResult<PaginatedResult<BookDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaginatedBooks([FromQuery] PaginationParams paginationParams)
        {
            var result = await _bookService.GetPaginatedBooksAsync(paginationParams);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
