using ManagmentApp.Models;
using ManagmentApp.Repositories;

namespace ManagmentApp.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }

    public class EmployeeService : IEmployeeService 
    {
        private readonly EmployeeRepo _employeeRepo;

        public EmployeeService(EmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }


        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepo.GetAllAsync();  

            return employees;
        }
    }
}
