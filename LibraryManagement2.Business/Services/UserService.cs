using LibraryManagement2.Business.Interfaces;
using LibraryManagement2.Data.Entities;
using LibraryManagement2.Data.Repositories.Interfaces;
using LibraryManagement2.Shared.DTOs;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement2.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<(bool Success, string Message)> RegisterUserAsync(RegisterDto request)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
                return (false, "Username already exists.");

            var newUser = new User
            {
                Username = request.Username,
                Role = request.Role
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, request.Password);

            await _userRepository.AddUserAsync(newUser);
            return (true, "User registered successfully.");
        }

        public async Task<(bool Success, string Message, User? User)> LoginUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return (false, "Invalid username or password.", null);

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Success)
                return (true, "Login successful.", user);

            return (false, "Invalid username or password.", null);
        }
    }
}
