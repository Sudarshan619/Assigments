using AutoMapper;
using Day28_EventBooking.DTO;
using Day28_EventBooking.Model;

namespace Day28_EventBooking.Mapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
        }

    }
}
