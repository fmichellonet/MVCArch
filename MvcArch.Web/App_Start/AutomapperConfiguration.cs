using AutoMapper;
using MvcArch.Web.Models;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MvcArch.Web.App_Start.AutomapperConfiguration), "Start")] 
namespace MvcArch.Web.App_Start {
    public static class AutomapperConfiguration
    {
        public static void Start() {

            Mapper.CreateMap<Domain.Employee, Employee>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => string.Format("{0} - {1}", src.FirstName, src.LastName)));

            Mapper.CreateMap<Employee, Domain.Employee>();
        }
    }
}