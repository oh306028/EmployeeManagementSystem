using AutoMapper;
using ManagmentApp.Dtos;
using ManagmentApp.Exceptions;
using ManagmentApp.Models;
using ManagmentApp.Repositories;

namespace ManagmentApp.Services
{
    public interface ICompensationService
    {
        Task<IEnumerable<Compensation>> GetAllSalariesAsync();
        Task<Compensation> GetEmployeeSalary(string employeeName);
        Task CreateCompensation(string employeeName, CreateCompensationDto dto);
    }

    public class CompensationService : ICompensationService
    {
        private readonly CompensationRepo _compensationRepo;
        private readonly EmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;

        public CompensationService(CompensationRepo compensationRepo, EmployeeRepo employeeRepo, IMapper mapper)
        {
            _compensationRepo = compensationRepo;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }
            
        public async Task<IEnumerable<Compensation>> GetAllSalariesAsync()  
        {
            var result = await _compensationRepo.GetAllAsync();

            return result;
        }

        public async Task<Compensation> GetEmployeeSalary(string employeeName)
        {
            var result = await _compensationRepo.GetAsync(employeeName);

            return result;
        }

        public async Task CreateCompensation(string employeeName, CreateCompensationDto dto)
        {

            var employeeToAddSalary = await _employeeRepo.GetByName(employeeName);

            if (employeeToAddSalary is null)
                throw new NotFoundException("Employee not found");  


            var mappedSalary = _mapper.Map<Compensation>(dto);
            mappedSalary.EmployeeId = employeeToAddSalary.Id;


            await _compensationRepo.CreateAsync(mappedSalary);

        }
            


    }
}
