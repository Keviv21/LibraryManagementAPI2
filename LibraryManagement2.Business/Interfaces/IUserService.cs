using LibraryManagement2.Data.Entities;
using LibraryManagement2.Shared.DTOs;

namespace LibraryManagement2.Business.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, string Message)> RegisterUserAsync(RegisterDto request);
        Task<(bool Success, string Message, User? User)> LoginUserAsync(string username, string password);
    }
}
