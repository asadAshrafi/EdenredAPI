using Edenred.DataAccess.Repositories;
using Edenred.Model;

namespace Edenred.Service.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> SaveAsync(UserDTO userDTO)
        {
            try
            {
                User user= new User()
                {
                    Id = userDTO.Id,
                    Name = userDTO.Name,
                    IsVerified= userDTO.IsVerified
                };
                if (userDTO.Id==0)
                {
                    var response = await _userRepository.AddAsync(user);
                    return SuccessResponse();
                }
                else
                {
                    bool isExists =await _userRepository.IsExistsAsync(userDTO.Id);
                    if (isExists)
                    {
                        await _userRepository.UpdateUser(user);
                        return SuccessResponse();
                    }
                    else
                    {
                        return FaultResponse(resultText:"User does not exist!");
                    }
                }
            }
            catch (Exception ex)
            {
                return FaultResponse();
                throw;
            }
        }
    }
}
