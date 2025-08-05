
using LibraryManagement2.Business.Interfaces;
using LibraryManagement2.Shared.DTO.MainData;
using LibraryManagement2.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace LibraryManagement2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="request">User registration data</param>
        /// <returns>Success message or errors</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ServiceOperationResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceOperationResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceOperationResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var result = await _userService.RegisterUserAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Login an existing user.
        /// </summary>
        /// <param name="request">User login credentials</param>
        /// <returns>JWT token on success</returns>
        [HttpPost("login")]
        [EnableRateLimiting("fixed")] // 🔒 Apply rate limiting policy
        [ProducesResponseType(typeof(ServiceOperationResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceOperationResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceOperationResult<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceOperationResult<string>), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ServiceOperationResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _userService.LoginUserAsync(request.Username, request.Password);

            if (!result.IsSuccess)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
