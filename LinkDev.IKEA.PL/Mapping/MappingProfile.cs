using AutoMapper;
using LinkDev.IKEA.BLL.Models;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.PL.ViewModels.Departments;

namespace LinkDev.IKEA.PL.Mapping
{
    public class MappingProfile : Profile
    {
      public MappingProfile()
        {

            #region Department
            CreateMap<DepartmentDetailsDTO, DepartmentViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
              

            #endregion


            #region Employee

         


            #endregion

        }
        
        



    }
}
