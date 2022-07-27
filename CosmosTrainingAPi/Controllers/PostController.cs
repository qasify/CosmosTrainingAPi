using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;
using CosmosTrainingAPi.Services;
using Microsoft.AspNetCore.Authorization;

namespace CosmosTrainingAPi.Controllers
{
    [Route("api/v{version:apiVersion}/Post/[action]")]
    [ApiController]
    [ApiVersion("1")]
    public class PostController : ControllerBase
    {
        private readonly ICosmosDBService _cosmosDBService;
        private readonly IUserService _userService;

        public PostController(ICosmosDBService cosmosDBService, IUserService userService)
        {
            _cosmosDBService = cosmosDBService;
            _userService = userService;
        }

        [HttpPost, Authorize(Roles = "User")]
        public async Task<ActionResult<string>> addPost(Post post)
        {
            string responce = await _cosmosDBService.createPost(post);
            return Ok(responce);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllposts()
        {
            var responce = await _cosmosDBService.GetAllposts();
            return Ok(responce);
        }

        [HttpDelete, Authorize(Roles = "User")]
        public async Task<ActionResult<string>> DeletePost(DeletePost post)
        {
            if (_userService.parseRoleFromToken().Contains("Admin") || _userService.parseNameFromToken() == post.Username)
            {
                var responce = await _cosmosDBService.DeletePost(post);
                return Ok(responce);
            }
            else
                return Unauthorized("");
        }
    }
}
