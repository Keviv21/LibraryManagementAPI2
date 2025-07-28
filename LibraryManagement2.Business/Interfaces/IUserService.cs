using LibraryManagement2.Data.Entities;
using LibraryManagement2.Shared.DTOs;

namespace LibraryManagement2.Business.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, string Message)> RegisterUserAsync(RegisterDto request);

        // Now also returns Token (string?)
        Task<(bool Success, string Message, string? Token, User? User)> LoginUserAsync(string username, string password);
    }
}
