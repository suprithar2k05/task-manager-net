using server.DTOs;
using server.Models;
using server.Repositories;
using server.Security;
namespace server.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }


    public async Task<User> AuthenticateAsync(UserLoginDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
            throw new Exception("Invalid credentials");

        return user;
    }

    public async Task<User> RegisterAsync(UserSignupDto dto)
    {
        // Check if user already exists
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new Exception("User already exists with this email.");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(dto.Password)
        };

        await _userRepository.CreateAsync(user);
        return user;
    }

    public async Task<User?> GetProfileAsync(string userId) {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        return user;
    }
}
