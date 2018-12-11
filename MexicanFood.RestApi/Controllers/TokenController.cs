using System.Linq;
using MexicanFood.Core.ApplicationService;
using MexicanFood.Core.Entities;
using MexicanFood.Infrastructure.Data.Repositories.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MexicanFood.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController: ControllerBase
    {
        private IUserService _userService;
        private IAuthenticationHelper _authenticationHelper;

        public TokensController(IUserService userService, IAuthenticationHelper authService)
        {
            _userService = userService;
            _authenticationHelper = authService;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            var user = _userService.GetUsers().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!_authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = _authenticationHelper.GenerateToken(user)
            });
        }
    }
}