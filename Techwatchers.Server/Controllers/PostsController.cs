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
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            //dodac var session=HttpContext.Session.GetId("SessionId");
            //cos takiego zeby sprawdzic czy user jest zalogowany
            //jesli tak to przekazuje id do frontu zeby moc potem wczytac na front odopwiedniego zalgowanego usera
            var posts = await _postRepository.GetPostsAsync();

            return Ok(posts);
        }
    }
}
