using ManagmentApp.Models;
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

        public ActionResult<IEnumerable<Employee>> GetAll()
        {

        }
    }

    
}
