using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Techwatchers.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterController(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (_registerRepository.IsUsernameOrEmailTaken(request.Username, request.Email))
            {
                return StatusCode(408,new { message = "Nazwa użytkownika lub email jest juz w użytku." });
            }
            if (!IsUsernameValid(request.Username)){
                return BadRequest(new { message = "Nazwa użytkownika musi zawierać co najmniej 3 znaki i składać się z liter, cyfr i podkreślników." });
            }
            if (!IsEmailValid(request.Email)){
                return BadRequest(new { message = "Niepoprawny adres email." });
            }
            if (!IsPasswordValid(request.Password)){
                return BadRequest(new { message = "Hasło musi zawierać co najmniej 1 cyfrę, 1 dużą literę, 1 znak specjalny i mieć co najmniej 8 znaków." });
            }
            var hashedPassword = PasswordHasher.HashPassword(request.Password);
            var user = new User
            {
                username = request.Username,
                email = request.Email,
                password = hashedPassword,
                statute = request.Statute
            };

            _registerRepository.RegisterUser(user);

            return Ok(new { message = "Rejestracja udana!" });
        }
        private bool IsPasswordValid(string password)
        {
            // Regex: At least 1 digit, 1 uppercase letter, 1 special character, and 8+ characters
            var passwordRegex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return passwordRegex.IsMatch(password);
        }
        private bool IsUsernameValid(string username)
        {
            // Regex to allow only letters, numbers, and underscores, and at least 3 characters long
            var usernameRegex = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9_]{3,}$");
            return usernameRegex.IsMatch(username);
        }
        private bool IsEmailValid(string email)
        {
            // Regex to ensure a valid email without whitespace
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            return emailRegex.IsMatch(email);
        }

    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Statute { get; set; }
    }
}