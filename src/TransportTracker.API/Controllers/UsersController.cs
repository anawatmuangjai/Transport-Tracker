using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportTracker.API.Infrastructure.Services.Interfaces;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [Authorize]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Authenticate to get token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest model)
        {
            var response = await userService.AuthenticateAsync(model.Username, model.Password);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticationRequest model)
        {
            var isExists = await userService.UserExistsAsync(model.Username);

            if (isExists)
            {
                return BadRequest(new { message = "Username already exists" });
            }

            var response = await userService.RegisterAsync(model.Username, model.Password);

            if (response == null)
            {
                return BadRequest(new { message = "Error when registering" });
            }

            return Ok();
        }
    }
}