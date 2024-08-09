using ManagmentApp.Models;
using ManagmentApp.Repositories;

namespace ManagmentApp.Services
{
    public interface ICompensationService
    {
        Task<IEnumerable<Compensation>> GetAllSalariesAsync();
        Task<Compensation> GetEmployeeSalary(string employeeName);
    }

    public class CompensationService : ICompensationService
    {
        private readonly CompensationRepo _compensationRepo;

        public CompensationService(CompensationRepo compensationRepo)
        {
            _compensationRepo = compensationRepo;
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


    }
}
