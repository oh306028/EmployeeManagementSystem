using AutoMapper;
using ManagmentApp.Dtos;
using ManagmentApp.Models;
using ManagmentApp.Repositories;
using ManagmentApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagmentApp.Controllers
{
    [ApiController]
    [Route("api/departaments")]
    public class DepartamentController : ControllerBase
    {
        private readonly DepartamentRepo _departamentRepository;
        private readonly IDepartamentService _departamentService;
        private readonly EmployeeRepo _employeeRepository;
        private readonly IMapper _mapper;

        public DepartamentController(DepartamentRepo departamentRepository, EmployeeRepo employeeRepository, IDepartamentService departamentService)
        {
            _departamentRepository = departamentRepository;
            _departamentService = departamentService;
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


        [HttpPost]
        public async Task<ActionResult> CreateDepartament([FromBody] CreateDepartamentDto dto)
        {
            await _departamentService.CreateDepartamentAsync(dto);  

            return Created("api/departaments",null);
        }


    }
}
