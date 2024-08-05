using ManagmentApp.Models;
using ManagmentApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ManagmentApp.Controllers
{
    [ApiController]
    [Route("api/departaments")]
    public class DepartamentController : ControllerBase
    {
        private readonly DepartamentRepo _departamentRepository;
        private readonly EmployeeRepo _employeeRepository;  

        public DepartamentController(DepartamentRepo departamentRepository, EmployeeRepo employeeRepository)
        {
            _departamentRepository = departamentRepository;
            _employeeRepository = employeeRepository;   
        }

        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetDepartmentsWithEmployees([FromQuery]string departamentName)
        {
           
            var results = await _employeeRepository.GetEmployeesByDepartament(departamentName);
 
            return Ok(results);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departament>>> GetDepartments()      
        {

            var results = await _departamentRepository.GetAllAsync();  

            return Ok(results);
        }


    }
}
