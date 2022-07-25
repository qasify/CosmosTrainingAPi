using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;
using CosmosTrainingAPi.Services;

namespace CosmosTrainingAPi.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllposts()
        {
            var responce = await _cosmosDBService.GetAllposts();
            return Ok(responce);
        }

        [HttpDelete]
        public async Task<ActionResult<List<User>>> DeletePost(DeletePost post)
        {
            var responce = await _cosmosDBService.DeletePost(post);
            return Ok(responce);
        }
    }
}
