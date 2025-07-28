using LibraryManagement2.Data.Entities;

namespace LibraryManagement2.Integration.Auth
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
