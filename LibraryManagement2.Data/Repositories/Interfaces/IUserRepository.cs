using LibraryManagement2.Data.Entities;

namespace LibraryManagement2.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}
