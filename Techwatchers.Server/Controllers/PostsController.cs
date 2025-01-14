using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Techwatchers.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts([FromQuery] string? category = null)
        {
            //dodac var session=HttpContext.Session.GetId("SessionId");
            //cos takiego zeby sprawdzic czy user jest zalogowany
            //jesli tak to przekazuje id do frontu zeby moc potem wczytac na front odopwiedniego zalgowanego usera
            var posts = await _postRepository.GetPostsAsync();

            if (!string.IsNullOrEmpty(category))
            {
                posts = posts.Where(p => p.category.name.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Ok(posts);
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
    }
}
