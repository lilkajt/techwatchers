using Microsoft.AspNetCore.Mvc;

namespace Techwatchers.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(IProfileRepository profileRepository, IHttpContextAccessor httpContextAccessor)
        {
            _profileRepository = profileRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("update-password")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var userId = session.GetInt32("id");

            if (userId == null)
            {
                return NotFound(new { message = "Użytkownik nie zalogowany." });
            }

            if (!IsPasswordValid(request.NewPassword))
            {
                return StatusCode(440, new { message = "Hasło musi zawierać co najmniej 1 cyfrę, 1 dużą literę, 1 znak specjalny i mieć co najmniej 8 znaków." });
            }

            var user = _profileRepository.GetUserById(userId);
            if (!PasswordHasher.VerifyPassword(request.CurrentPassword, user.password))
            {
                return Unauthorized(new { message = "Niepoprawne aktualne hasło." });
            }

            if (PasswordHasher.VerifyPassword(request.NewPassword, user.password))
            {
                return BadRequest(new { message = "Nowe hasło nie może być takie samo jak aktualne hasło." });
            }

            var hashedNewPassword = PasswordHasher.HashPassword(request.NewPassword);
            _profileRepository.UpdatePassword(user, hashedNewPassword);

            return Ok(new { message = "Hasło zostało zaktualizowane." });
        }

        private bool IsPasswordValid(string password)
        {
            var passwordRegex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{8,}$");
            return passwordRegex.IsMatch(password);
        }
    }

    public class UpdatePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}