using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;
using CosmosTrainingAPi.Services;

namespace CosmosTrainingAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ICosmosDBService _cosmosDBService;

        public UserController(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateNewUser(UserDTO user)
        {
            string responce = await _cosmosDBService.CreateNewUser(user);
            return Ok(responce);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllusers()
        {
            var responce = await _cosmosDBService.GetAllusers();
            return Ok(responce);
        }

        [HttpPatch]
        public async Task<ActionResult<string>> UpdateUserPassword(UpdatePassword user)
        {
            var responce = await _cosmosDBService.UpdateUserPassword(user);
            return Ok(responce);
        }

    }
}
