using LibraryManagement2.Shared.DTO.MainData;
using LibraryManagement2.Shared.Response;

namespace LibraryManagement2.Business.Interfaces
{
    public interface IUserService
    {
        Task<ServiceOperationResult<RegisterResponseDto>> RegisterUserAsync(RegisterDto request);
        Task<ServiceOperationResult<string>> LoginUserAsync(string username, string password);
    }
}
