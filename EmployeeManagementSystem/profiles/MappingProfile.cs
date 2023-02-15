using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SoccerGame.profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<DepartmentForCreationDto, Department>().ReverseMap();
            CreateMap<DepartmentForUpdateDto, Department>().ReverseMap();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeForCreationDto, Employee>().ReverseMap();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<Manager, ManagerDto>();
            CreateMap<ManagerForCreationDto, Manager>().ReverseMap();
            CreateMap<ManagerForUpdateDto, Manager>().ReverseMap();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
