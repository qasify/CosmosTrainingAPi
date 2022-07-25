using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;
using CosmosTrainingAPi.Services;
using Microsoft.AspNetCore.Authorization;

namespace CosmosTrainingAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private ICosmosDBService _cosmosDBService;

        public PostController(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> addPost(Post post)
        {
            string responce = await _cosmosDBService.createPost(post);
            return Ok(responce);
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<UserDTO>>> GetAllposts()
        {
            var responce = await _cosmosDBService.GetAllposts();
            return Ok(responce);
        }

        [HttpDelete]
        public async Task<ActionResult<List<UserDTO>>> DeletePost(DeletePost post)
        {
            var responce = await _cosmosDBService.DeletePost(post);
            return Ok(responce);
        }
    }
}
