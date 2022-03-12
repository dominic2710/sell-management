using Microsoft.AspNetCore.Mvc;
using SellManagement.Api.Models;
using SellManagement.Api.Services;
using SellManagement.Api.Messages;

namespace SellManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = VNIMessages.MSG_LOGIN_001 });

            return Ok(response);
        }
    }
}
