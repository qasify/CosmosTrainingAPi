using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;
using CosmosTrainingAPi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CosmosTrainingAPi.Controllers.v2
{
    [ApiVersion("2")]
    [Route("/api/v{version:apiVersion}/[controller]")]
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

        [HttpGet("GetRole"), Authorize(Roles = "User")]
        public ActionResult<string[]> GetRole()
        {
            var userRoles = _userService.parseRoleFromToken();
            return Ok(userRoles);
        }

    }
}
