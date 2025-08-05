using AutoMapper;
using LibraryManagement2.Business.Interfaces;
using LibraryManagement2.Data.Entities;
using LibraryManagement2.Data.Repositories.Interfaces;
using LibraryManagement2.Integration.Auth;
using LibraryManagement2.Shared.DTO.MainData;
using LibraryManagement2.Shared.Response;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement2.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ServiceOperationResult<RegisterResponseDto>> RegisterUserAsync(RegisterDto request)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                return ServiceOperationResult<RegisterResponseDto>.FailureResult("User already exists.");
            }

            var user = _mapper.Map<User>(request);

            user.Password = _passwordHasher.HashPassword(null!, request.Password);
         

            await _userRepository.AddUserAsync(user);
            var responseDto = _mapper.Map<RegisterResponseDto>(user);


            return ServiceOperationResult<RegisterResponseDto>.SuccessResult(responseDto, "User registered successfully.");
        }

        public async Task<ServiceOperationResult<string>> LoginUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                return ServiceOperationResult<string>.FailureResult("Invalid username or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(null!, user.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return ServiceOperationResult<string>.FailureResult("Invalid username or password.");
            }

            var userDto = _mapper.Map<UserTokenDto>(user);

            var token = _tokenService.GenerateToken(userDto);

            return ServiceOperationResult<string>.SuccessResult(token, "Login successful.");
        }
    }
}
