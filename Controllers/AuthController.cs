using Domain.Dto;
using Domain.Entities;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Services.AuthService;

namespace passometro_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(RegisterDto user)
        {

            var response = await authService.Register(new User
            {
                Email = user.Email,
            },
            user.Password);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDto login)
        {
            var response = await authService.Login(login.Email, login.Password);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
