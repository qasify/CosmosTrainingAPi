using Microsoft.AspNetCore.Mvc;
using PracticeMVCApplication.Models;
using PracticeMVCApplication.Services;

namespace CosmosTrainingAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ICosmosDBService _cosmosDBService;

        public HomeController(ICosmosDBService cosmosDBService)
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

        /*        

                //[HttpPost("GetNurces")]
                //public async Task<ActionResult<List<Nurse>>> GetNurces(NursesDemand demand)
                //{
                //    return Ok(Supplier1.getNurses(demand));
                //}

                [HttpPost("AddNewNurse")]
                public async Task<ActionResult<List<Nurse>>> AddNewNurse(Nurse nurse)
                {
                    Supplier1.addNurse(nurse);
                    return Ok(Supplier1.getAvailableNurses());
                }*/


    }
}
