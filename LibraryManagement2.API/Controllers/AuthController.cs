using LibraryManagement2.Business.Interfaces;
using LibraryManagement2.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace LibraryManagement2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            _logger.LogInformation("Registration attempt for username: {Username}", request.Username);

            var (success, message) = await _userService.RegisterUserAsync(request);

            if (!success)
            {
                _logger.LogWarning("Registration failed for username: {Username}", request.Username);
                return BadRequest(new { message });
            }

            _logger.LogInformation("Registration successful for username: {Username}", request.Username);
            return Ok(new { message });
        }

        /// <summary>
        /// Authenticates the user and returns a JWT token.
        /// </summary>
        [HttpPost("login")]
        [EnableRateLimiting("fixed")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            _logger.LogInformation("Login attempt for user: {Username}", request.Username);

            var (success, message, token, userTokenDto) = await _userService.LoginUserAsync(request.Username, request.Password);

            if (!success || userTokenDto== null || string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("Login failed for user: {Username}", request.Username);
                return Unauthorized(new { message });
            }

            _logger.LogInformation("Login successful for user: {Username}", userTokenDto.Username);

            return Ok(new
            {
                token,
                user = userTokenDto
            });
        }
    }
}
