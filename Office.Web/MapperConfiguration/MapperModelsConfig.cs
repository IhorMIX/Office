using Office.BLL.Models;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Selections;
using Office.Web.Models;

namespace Office.Web.MapperConfiguration;

public class MapperModelsConfig : AutoMapper.Profile
{
    public MapperModelsConfig()
    {
        CreateMap<BaseEmployee, BaseEmployeeModel>().ReverseMap();
        CreateMap<Employee, EmployeeModel>().ReverseMap();
        CreateMap<Employee, BaseEmployeeModel>().ReverseMap();
        CreateMap<EmployeeModel, BaseEmployeeModel>().ReverseMap();
        CreateMap<BaseManager, BaseManagerModel>().ReverseMap();
        CreateMap<HrManager, HrManagerModel>().ReverseMap();
        CreateMap<ProjectManager, ProjectManagerModel>().ReverseMap();
        
        CreateMap<AuthorizationInfoModel, AuthorizationInfo>()
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
            .ForMember(dest => dest.Employee, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<Position, PositionModel>().ReverseMap();
        CreateMap<Subdivision, SubdivisionModel>().ReverseMap();
        
        CreateMap<EmployeeViewModel, EmployeeModel>().ReverseMap();
        CreateMap<EmployeeCreateModel, EmployeeModel>().ReverseMap();
        
        CreateMap<SubdivisionModel, SelectionViewModel>().ReverseMap();
        CreateMap<PositionModel, SelectionViewModel>().ReverseMap();
        
        CreateMap<BaseEmployeeModel, CurrentUserViewModel>()
            .ForMember(dest => dest.EmployeeType, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<BaseEmployee, CurrentUserViewModel>()
            .ForMember(dest => dest.EmployeeType, opt => opt.Ignore())
            .ReverseMap();
    }
}