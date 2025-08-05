
using LibraryManagement2.Shared.DTO.MainData;

namespace LibraryManagement2.Integration.Auth
{
    public interface ITokenService
    {
        string GenerateToken(UserTokenDto user);
    }
}
