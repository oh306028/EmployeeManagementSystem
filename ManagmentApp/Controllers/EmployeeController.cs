using ManagmentApp.Models;
using ManagmentApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagmentApp.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var emloyees = await _employeeService.GetAllEmployeesAsync();

            return Ok(emloyees);
        }


        [HttpPost]
        public async Task<ActionResult> Create(Employee newEmployee)
        {
            await _employeeService.CreateEmployee(newEmployee);

            return NoContent();
        }
            

    }

    
}
