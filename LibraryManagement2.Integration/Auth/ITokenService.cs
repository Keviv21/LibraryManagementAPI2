
using LibraryManagement2.Shared.DTOs;

namespace LibraryManagement2.Integration.Auth
{
    public interface ITokenService
    {
        string GenerateToken(UserTokenDto user);
    }
}
