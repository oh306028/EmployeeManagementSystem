using AutoMapper;
using ManagmentApp.Dtos;
using ManagmentApp.Models;
using ManagmentApp.Repositories;
using ManagmentApp.Services;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;

namespace ManagmentApp.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepo _departmentRepository;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(DepartmentRepo DepartmentRepository, IDepartmentService departmentService)
        {
            _departmentRepository = DepartmentRepository;
            _departmentService = departmentService;     
                
        }   

        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartmentsWithEmployees()
        {

            var results = await _departmentRepository.GetEmployeesForEachDepartment();    
 
            return Ok(results);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()      
        {

            var results = await _departmentRepository.GetAllAsync();  

            return Ok(results);
        }


        [HttpPost]
        public async Task<ActionResult> CreateDepartment([FromBody] CreateDepartmentDto dto)
        {
            await _departmentService.CreateDepartmentAsync(dto);    

            return Created("api/departments",null);
        }


    }
}
