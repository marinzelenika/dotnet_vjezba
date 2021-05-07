using System.Threading.Tasks;
using dotnet_vjezba.Services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using dotnet_vjezba.Dtos.Post;

namespace dotnet_vjezba.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        
        [HttpGet("GetAllPostsByUser")]
        public async Task<IActionResult> Get()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _postService.GetAllPostsByUser(id));
        }
        
        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _postService.GetAllPosts());
        }
        
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostDto newPost)
        {
            return Ok(await _postService.AddPost(newPost));
        }
    }
}