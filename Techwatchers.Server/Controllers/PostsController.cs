using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Techwatchers.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ILoginRepository _loginRepository;
        private IHttpContextAccessor _httpContextAccessor;


        public PostsController(IPostRepository postRepository, ILoginRepository loginRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postRepository = postRepository;
            _loginRepository = loginRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 5, string? category = null)
        {
            //dodac var session=HttpContext.Session.GetId("SessionId");
            //cos takiego zeby sprawdzic czy user jest zalogowany
            //jesli tak to przekazuje id do frontu zeby moc potem wczytac na front odopwiedniego zalgowanego usera
            // var posts = await _postRepository.GetPostsAsync();

            // return Ok(posts);
            try
            {
                var (posts, totalCount) = await _postRepository.GetPostsAsync(page, pageSize);

                if (!string.IsNullOrEmpty(category))
                {
                    posts = posts.Where(p => p.category.name.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                return Ok(new
                {
                    posts,
                    totalCount
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPosts: {ex.Message}");
                return StatusCode(500, new { message = "Wystąpił błąd podczas pobierania postów." });
            }
                }
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(string id)
        {
            if (!int.TryParse(id, out int postId))
            {
                return BadRequest("Invalid post id");
            }
            var post = await _postRepository.GetPostByIdAsync(postId);
            return Ok(post);
        }

        [HttpPost("{id}/toggle-like")]
        public async Task<IActionResult> ToggleLike(int id, [FromBody] ToggleLikeRequest request)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.Session;

                var userId = session.GetInt32("id");

                Console.WriteLine(userId);
                
                Console.WriteLine(userId);
                if (userId.Equals(null))
                {
                    return Unauthorized(new { message = "User not logged in" });
                }

                var user = await _loginRepository.GetUserByIdAsync(userId.Value);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid user" });
                }

                var post = await _postRepository.GetPostByIdAsync(id);
                if (post == null)
                {
                    return NotFound(new { message = "Post not found" });
                }

                if (request.Liked)
                {
                    await _postRepository.AddLikeAsync(post.id);
                }
                else
                {
                    await _postRepository.RemoveLikeAsync(post.id);
                }

                return Ok(new { message = "Like state updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ToggleLike: {ex.Message}");
                return StatusCode(500, new { message = "Wystąpił błąd podczas aktualizacji stanu lajków." });
            }
        }
    }

    public class ToggleLikeRequest
    {
        public bool Liked { get; set; }
    }
}
