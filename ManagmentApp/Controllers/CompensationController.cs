using ManagmentApp.Models;
using ManagmentApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagmentApp.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class CompensationController : ControllerBase
    {
        private readonly ICompensationService _compensationService;

        public CompensationController(ICompensationService compensationService)
        {
            _compensationService = compensationService; 
        }

        [HttpGet("salaries")]
        public async Task<ActionResult<IEnumerable<Compensation>>> GetSalaries()
        {
            var result = await _compensationService.GetAllSalariesAsync();

            return Ok(result);
        }

        //TO DO:
        //IMPLEMENT METHODS BELOW
        //!!!!

        //IMPLEMENT 
        //IMPLEMENT 

        /*
        [HttpGet("{employeeName}/salaries")]    
        public ActionResult<IEnumerable<Compensation>> GetSalary([FromRoute] string employeeName)
        {

        }
         //IMPLEMENT 
         //IMPLEMENT 

        [HttpPost("{employeeName}/salaries")]
        public ActionResult<IEnumerable<Compensation>> CreateSalary([FromRoute] string employeeName)
        {   

        }
        */


    }
}
