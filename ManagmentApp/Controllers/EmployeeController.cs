using ManagmentApp.Dtos;
using ManagmentApp.Models;
using ManagmentApp.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
        public async Task<ActionResult<string>> GetAll()
        {
            var emloyees = await _employeeService.GetAllEmployeesAsync();

            return Ok(emloyees);            
        }


        [HttpPost]  
        public async Task<ActionResult> Create([FromBody]CreateEmployeeDto dto) 
        {
            await _employeeService.CreateEmployee(dto);

            return NoContent();
        }


        [HttpGet("details")]
        public async Task<ActionResult<List<EmployeeWithDetails>>> GetEmpWithDetails()
        {
            var results = await _employeeService.GetDetails();

            return Ok(results); 
        }

    }

    
}
