using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;
using CosmosTrainingAPi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CosmosTrainingAPi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICosmosDBService _cosmosDBService;
        private readonly IUserService _userService;

        public AuthenticationController(ICosmosDBService cosmosDBService, IUserService userService)
        {
            _cosmosDBService = cosmosDBService;
            _userService = userService;
        }

        [HttpPost("AuthenciateUser")]
        public async Task<ActionResult<string>> AuthenciateUser(UserCredentials user)
        {
            var responce = await _cosmosDBService.AuthenciateUser(user);
            return Ok(responce);
        }

        [HttpPost("CreateNewUser")]
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
