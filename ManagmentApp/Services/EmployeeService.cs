using AutoMapper;
using ManagmentApp.Dtos;
using ManagmentApp.Models;
using ManagmentApp.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ManagmentApp.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task CreateEmployee(CreateEmployeeDto dto);
        Task<List<EmployeeWithDetails>> GetDetails();

     }  

    public class EmployeeService : IEmployeeService 
    {
        private readonly EmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;
      

        public EmployeeService(EmployeeRepo employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        
        }       

        public async Task<List<EmployeeWithDetails>> GetDetails()
        {
            var results = await _employeeRepo.GetEmployeeWithCompensation();

            return results; 
        }


        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()           
        {

            var employees = await _employeeRepo.GetAllAsync();


            return employees;
        }

        public async Task CreateEmployee(CreateEmployeeDto dto)
        {

            var mappedEmploye = _mapper.Map<Employee>(dto);

            mappedEmploye.HireDate = DateTime.Now;
            await _employeeRepo.CreateAsync(mappedEmploye);
        }
    }
}
