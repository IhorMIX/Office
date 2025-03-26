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
        CreateMap<EmployeeViewModel, EmployeeModel>().ReverseMap();
        CreateMap<EmployeeCreateModel, EmployeeModel>().ReverseMap();
        CreateMap<EmployeeFullViewModel, EmployeeModel>().ReverseMap();

        CreateMap<BaseManager, BaseManagerModel>().ReverseMap();
        CreateMap<HrManager, HrManagerModel>().ReverseMap();
        CreateMap<ProjectManager, ProjectManagerModel>().ReverseMap();

        CreateMap<BaseEmployeeModel, CurrentUserViewModel>()
            .ForMember(dest => dest.EmployeeType, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<BaseEmployee, CurrentUserViewModel>()
            .ForMember(dest => dest.EmployeeType, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<BaseEmployee, ManagerDetailViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();

        CreateMap<HrManager, ManagerDetailViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();
        
        CreateMap<HrManagerViewModel, HrManagerModel>()
            .ReverseMap();
        
        CreateMap<ProjectManager, ManagerDetailViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();

        CreateMap<ProjectManagerViewModel, ProjectManagerModel>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, ManagerCreateModel>().ReverseMap();
        CreateMap<ProjectManagerModel, ManagerCreateModel>().ReverseMap();
        CreateMap<HrManagerModel, ManagerCreateModel>().ReverseMap();

        CreateMap<BaseManagerModel, ManagerViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();

        CreateMap<BaseManager, ManagerViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();

        CreateMap<BaseManagerModel, HrManager>().ReverseMap();

        CreateMap<AuthorizationInfoModel, AuthorizationInfo>()
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
            .ForMember(dest => dest.Employee, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Position, PositionModel>().ReverseMap();
        CreateMap<PositionModel, PositionViewModel>().ReverseMap();
        CreateMap<PositionCreateModel, PositionModel>().ReverseMap();
        
        CreateMap<Subdivision, SubdivisionModel>().ReverseMap();
        CreateMap<SubdivisionModel, SubdivisionViewModel>().ReverseMap();
        CreateMap<SubdivisionCreateModel, SubdivisionModel>().ReverseMap();
        
        CreateMap<SubdivisionModel, SelectionViewModel>().ReverseMap();
        CreateMap<PositionModel, SelectionViewModel>().ReverseMap();
    }
}
