using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeMVCApplication.Models;
using PracticeMVCApplication.Services;

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
            return Ok(_cosmosDBService.createPost(post));
        }
    }
}
