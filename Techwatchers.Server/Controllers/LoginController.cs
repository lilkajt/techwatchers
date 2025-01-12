using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Techwatchers.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public LoginController(ILoginRepository loginRepository, IHttpContextAccessor httpContextAccessor)
        {
            _loginRepository = loginRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var userPassword = _loginRepository.GetUserPassword(request.Username);
                if (userPassword == null)
                {
                    return Unauthorized(new { message = "Niepoprawna nazwa użytkownika lub hasło!" });
                }
                if (PasswordHasher.VerifyPassword(request.Password, userPassword))
                {
                    var user = _loginRepository.ValidateUser(request.Username, userPassword);
                    var session = _httpContextAccessor.HttpContext.Session;

                    session.SetInt32("id", user.id);
                    session.SetString("username", user.username);

                    return Ok(new { message = "Login udany!", user });
                }
                return Unauthorized(new { message = "Niepoprawna nazwa użytkownika lub hasło!" });
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { message = "Wystąpił błąd!" });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.Session;
                session.Clear();
                return Ok(new { message = "Wylogowano!" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "Wystąpił błąd!", details = ex.Message });
            }
        }
 
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}