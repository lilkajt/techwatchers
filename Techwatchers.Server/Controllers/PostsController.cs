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
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            //dodac var session=HttpContext.Session.GetId("SessionId");
            //cos takiego zeby sprawdzic czy user jest zalogowany
            //jesli tak to przekazuje id do frontu zeby moc potem wczytac na front odopwiedniego zalgowanego usera
            // var posts = await _postRepository.GetPostsAsync();

            // return Ok(posts);
            try
            {
                var (posts, totalCount) = await _postRepository.GetPostsAsync(page, pageSize);

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
            }
}
