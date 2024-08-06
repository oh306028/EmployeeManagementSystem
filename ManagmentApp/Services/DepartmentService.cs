using AutoMapper;
using ManagmentApp.Dtos;
using ManagmentApp.Models;
using ManagmentApp.Repositories;

namespace ManagmentApp.Services
{
    public interface IDepartmentService
    {
        Task CreateDepartmentAsync(CreateDepartmentDto dto);
        Task<List<DepartmentDto>> GetDepartmentsWithEmp();

    }   
    public class DepartmentService : IDepartmentService
    {
        private readonly DepartmentRepo _DepartmentRepo;
        private readonly IMapper _mapper;

        public DepartmentService(DepartmentRepo DepartmentRepo, IMapper mapper)
        {
            _DepartmentRepo = DepartmentRepo;
            _mapper = mapper;
        }

        public async Task CreateDepartmentAsync(CreateDepartmentDto dto)
        {
            var result = _mapper.Map<Department>(dto);

            await _DepartmentRepo.CreateAsync(result); 
        }

        public async Task<List<DepartmentDto>> GetDepartmentsWithEmp()
        {
            var results = await _DepartmentRepo.GetEmployeesForEachDepartment();

            return results;
        }

    }
}
