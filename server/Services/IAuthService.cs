using server.DTOs;
using System.Threading.Tasks;
using server.Models;

namespace server.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserSignupDto dto);
        Task<User> AuthenticateAsync(UserLoginDto request);

        Task<User?> GetProfileAsync(string userId);

    }
}
