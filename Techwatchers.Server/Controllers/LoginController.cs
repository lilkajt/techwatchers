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
            try
            {
                var userPassword = _loginRepository.GetUserPassword(request.Username);
                if ( userPassword == null) {
                    return Unauthorized(new { message = "Niepoprawna nazwa użytkownika lub hasło!" });
                }
                if (PasswordHasher.VerifyPassword(request.Password, userPassword))
                {
                    //musimy zwrocic uzytkownika
                    var user = _loginRepository.ValidateUser(request.Username, userPassword);
                    return Ok(new { message = "Login udany!", user });
                }
                return Unauthorized(new { message = "Niepoprawna nazwa użytkownika lub hasło!" });
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { message = "Wystąpił błąd!" });
            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}