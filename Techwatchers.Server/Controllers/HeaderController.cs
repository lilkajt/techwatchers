using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Techwatchers.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly IHeaderRepository _headerRepository;
        public HeaderController(IHeaderRepository headerRepository)
        {
            _headerRepository = headerRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _headerRepository.GetCategoriesAsync();

            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> AddPostAsync([FromBody] HeaderRequest request ){
            request.Title = TrimSpaces(request.Title);
            request.Description = TrimSpaces(request.Description);

            // test co wypisuje podczas dodawania posta
            Console.WriteLine(request.Title);
            Console.WriteLine(request.Description);
            Console.WriteLine(request.CategoryId);
            Console.WriteLine(request.UserId);
            if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Description))
            {
                return StatusCode(440,"Tytuł i opis jest wymagany.");
            }
            if (request.CategoryId == 0 || request.UserId == 0)
            {
                return StatusCode(443, "Kategoria i użytkownik są wymagane.");
            }
            bool categoryExists = await _headerRepository.CategoryExistsAsync(request.CategoryId);
            if (!categoryExists)
            {
                return StatusCode(441, "Podana kategoria nie istnieje.");
            }
            // temporary solution - normally we use session to get u_id and authorize so noone can add post of different user to db using backend
            bool userExists = await _headerRepository.UserExistsAsync(request.UserId);
            if (!userExists)
            {
                return StatusCode(442, "Podany użytkownik nie istnieje.");
            }
            bool success = await _headerRepository.AddPostAsync(request.Title, request.Description, request.CategoryId, request.UserId);
            if (success)
            {
                return Ok(new { message = "Post opublikowany" });
            }

            return StatusCode(500, "Wystąpił błąd podczas publikowania posta.");
        }
        private string TrimSpaces(string input)
        {
            return input?.Trim();
        }
    }
    public class HeaderRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
