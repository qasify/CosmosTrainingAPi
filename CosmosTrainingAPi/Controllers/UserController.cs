using Microsoft.AspNetCore.Mvc;
using PracticeMVCApplication.Models;
using PracticeMVCApplication.Services;

namespace CosmosTrainingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ICosmosDBService _cosmosDBService;

        public UserController(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        [HttpPost("CreateNewUser")]
        public async Task<ActionResult<string>> AddNewNurse(User user)
        {
            string responce = await _cosmosDBService.CreateNewUser(user);
            return Ok(responce);
        }

        [HttpGet("GetAllusers")]
        public async Task<ActionResult<List<User>>> GetAllusers()
        {
            var responce = await _cosmosDBService.GetAllusers();
            return Ok(responce);
        }

        [HttpPost("AuthenciateUser")]
        public async Task<ActionResult<string>> AuthenciateUser(UserCredentials user)
        {
            var responce = await _cosmosDBService.AuthenciateUser(user);
            return Ok(responce);
        }


    }
}
