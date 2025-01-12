using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Techwatchers.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILoginRepository loginRepository, IHttpContextAccessor httpContextAccessor)
        {
            _loginRepository = loginRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("current-user")]
        public IActionResult GetCurrentUser()
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.Session;
                var userId = session.GetInt32("id");
                var username = session.GetString("username");
                Console.WriteLine(userId + username);

                if (userId == null || username == null)
                {
                    return Accepted(new { isLoggedIn = false, user = (object)null });
                }

                var user = new
                {
                    Id = userId,
                    Username = username
                };

                return Ok(new { isLoggedIn = true, user });
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { message = "Wystąpił błąd!" });
            }
        }
    }
}