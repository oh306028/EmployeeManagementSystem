using AutoMapper;
using ManagmentApp.Dtos;
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
        private readonly IMapper _mapper;

        public DepartamentController(DepartamentRepo departamentRepository, EmployeeRepo employeeRepository, IMapper mapper)
        {
            _departamentRepository = departamentRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
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


        [HttpPost]
        public async Task<ActionResult> CreateDepartament([FromBody] CreateDepartamentDto dto)
        {
            var result = _mapper.Map<Departament>(dto);

            await _departamentRepository.CreateAsync(result);

            return Created("api/departaments",null);
        }


    }
}
