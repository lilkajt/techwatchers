using Microsoft.AspNetCore.Mvc;

namespace Techwatchers.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _loginRepository.ValidateUser(request.Username, request.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { message = "Login successful", user });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}