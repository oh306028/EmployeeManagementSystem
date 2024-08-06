using ManagmentApp.Models;
using ManagmentApp.Repositories;

namespace ManagmentApp.Services
{
    public interface ICompensationService
    {
        Task<IEnumerable<Compensation>> GetAllSalariesAsync();  
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


    }
}
