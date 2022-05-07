using Domain.Entities;
using Domain.Response;

namespace Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<User> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
    }
}
