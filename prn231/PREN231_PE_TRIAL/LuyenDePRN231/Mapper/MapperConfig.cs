using AutoMapper;
using LuyenDePRN231.DTO;
using LuyenDePRN231.Models;

namespace LuyenDePRN231.Mapper
{
    public class MapperConfig : Profile
    {
       public MapperConfig() {
            CreateMap<Order, OrderDTO>()
                    .ForMember(x => x.EmployeeName, y => y.MapFrom(src => src.Employee.FirstName + src.Employee.LastName))
                    .ForMember(x => x.CustomerName, y => y.MapFrom(src => src.Customer.ContactName)).ReverseMap();
          
            CreateMap<Employee, EmployeeDTO>()
                        .ForMember(x => x.DepartmentName, y => y.MapFrom(src => src.Department.DepartmentName)).ReverseMap();
                        



        }
    }
}
