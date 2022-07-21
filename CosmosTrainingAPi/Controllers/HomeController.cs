using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

/*        [HttpGet("ShowAvailableNurces")]
        public async Task<ActionResult<List<Nurse>>> ShowAvailableNurces()
        {
            return Ok(Supplier1.getAvailableNurses());
        }

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
