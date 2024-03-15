using Edenred.Model;

namespace Edenred.Service.Services
{
    public interface IUserService
    {
        Task<Response> SaveAsync(UserDTO userDTO); 
    }
}