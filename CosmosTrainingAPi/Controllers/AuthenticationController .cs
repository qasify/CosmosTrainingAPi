using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;
using CosmosTrainingAPi.Services;

namespace CosmosTrainingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ICosmosDBService _cosmosDBService;

        public AuthenticationController(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AuthenciateUser(UserCredentials user)
        {
            var responce = await _cosmosDBService.AuthenciateUser(user);
            return Ok(responce);
        }

    }
}
