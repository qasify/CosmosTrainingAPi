
using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;
using CosmosTrainingAPi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CosmosTrainingAPi.Controllers
{
    [Route("api/v{version:apiVersion}/Authentication/[action]")]
    [ApiController]
    [ApiVersion("2")]
    public class AuthenticationV2Controller : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<string>> AuthenciateUser(UserCredentials user)
        {
            return Ok("AuthenciateV2User");
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateNewUser(UserDTO user)
        {
            return Ok("CreateNewUser");
        }


        [HttpGet, Authorize(Roles = "User")]
        public ActionResult<string> GetName()
        {
            return Ok("GetName");
        }

        [HttpGet, Authorize(Roles = "User")]
        public ActionResult<string> GetRole()
        {
            return Ok("GetRole");
        }
    }
}