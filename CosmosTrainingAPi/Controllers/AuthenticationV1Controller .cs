using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;
using CosmosTrainingAPi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CosmosTrainingAPi.Controllers
{
    [Route("api/v{version:apiVersion}/Authentication/[action]")]
    [ApiController]
    [ApiVersion("1")]
    public class AuthenticationV1Controller : ControllerBase
    {
        private readonly ICosmosDBService _cosmosDBService;
        private readonly IUserService _userService;

        public AuthenticationV1Controller(ICosmosDBService cosmosDBService, IUserService userService)
        {
            _cosmosDBService = cosmosDBService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AuthenciateUser(UserCredentials user)
        {
            var responce = await _cosmosDBService.AuthenciateUser(user);
            return Ok(responce);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateNewUser(UserDTO user)
        {
            string responce = await _cosmosDBService.CreateNewUser(user);
            return Ok(responce);
        }


        [HttpGet, Authorize(Roles = "User")]
        public ActionResult<string> GetName()
        {
            var userName = _userService.parseNameFromToken();
            return Ok(userName);
        }

        [HttpGet, Authorize(Roles = "User")]
        public ActionResult<string[]> GetRole()
        {
            var userRoles = _userService.parseRoleFromToken();
            return Ok(userRoles);
        }
    }
}
