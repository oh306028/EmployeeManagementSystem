using AutoMapper;
using ManagmentApp.Dtos;
using ManagmentApp.Models;
using ManagmentApp.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;

namespace ManagmentApp.Services
{
    public interface IEmployeeService
    {
        Task<string> GetAllEmployeesAsync();     
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


        public async Task<string> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepo.GetEmployeeWithDepartaments();

            // var dotNetObjList = employees.ConvertAll(BsonTypeMapper.MapToDotNetValue);

            var settings = new JsonWriterSettings { Indent = true };
            var jsonDocuments = employees.Select(e => e.ToJson(settings)).ToList();
            var jsonArray = "[" + string.Join(",", jsonDocuments) + "]";

            return jsonArray;
        }


        public async Task CreateEmployee(CreateEmployeeDto dto)
        {

            var mappedEmploye = _mapper.Map<Employee>(dto);

            mappedEmploye.HireDate = DateTime.Now;
            await _employeeRepo.CreateAsync(mappedEmploye);
        }
    }
}
