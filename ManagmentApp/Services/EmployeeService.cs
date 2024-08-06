using AutoMapper;
using ManagmentApp.Dtos;
using ManagmentApp.Exceptions;
using ManagmentApp.Models;
using ManagmentApp.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;

namespace ManagmentApp.Services
{
    public interface IEmployeeService
    {
        Task CreateEmployee(CreateEmployeeDto dto);
        Task<List<EmployeeWithDetails>> GetDetails();
            
     }      

    public class EmployeeService : IEmployeeService 
    {
        private readonly EmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;
        private readonly DepartmentRepo _DepartmentRepo;

        public EmployeeService(EmployeeRepo employeeRepo, IMapper mapper, DepartmentRepo DepartmentRepo)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _DepartmentRepo = DepartmentRepo;
        }       

        public async Task<List<EmployeeWithDetails>> GetDetails()
        {
            var results = await _employeeRepo.GetEmployeeWithCompensation();

            return results; 
        }


   
        public async Task CreateEmployee(CreateEmployeeDto dto)
        {

            var mappedEmploye = _mapper.Map<Employee>(dto);       
            var Department = await _DepartmentRepo.GetByNameAsync(dto.DepartmentName);

            if (Department is null)
                throw new NotFoundException("Cannot find that Department");

            mappedEmploye.DepartmentId = Department.Id;
            mappedEmploye.HireDate = DateTime.Now;

            await _employeeRepo.CreateAsync(mappedEmploye);
        }
    }
}
