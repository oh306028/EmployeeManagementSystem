using AutoMapper;
using ManagmentApp.Dtos;
using ManagmentApp.Models;

namespace ManagmentApp
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<CreateCompensationDto, Compensation>();   

        }
    }
}
