using Azure;
using Edenred.Model;
using Edenred.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdenredAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(UserDTO userDTO)
        {
            var response = await _userService.SaveAsync(userDTO);
            return Ok(response);
        }
    }
}
