using AutoMapper;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.web.Models
{
    public class EmpoyeeProfile : Profile
    {
        public EmpoyeeProfile()
        {
            CreateMap<Employee, EmployeeEditModel>()
                .ForMember(dest => dest.ConfirmEmail,
                opt => opt.MapFrom(src => src.Email));
            CreateMap<EmployeeEditModel, Employee>();
        }

    }
}
