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

        CreateMap<SelectionCreateModel, PositionModel>();
        CreateMap<Position, PositionModel>().ReverseMap();
        CreateMap<PositionModel, SelectionViewModel>().ReverseMap();
        CreateMap<SelectionViewModel, Position>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        
        CreateMap<SelectionCreateModel, SubdivisionModel>();
        CreateMap<Subdivision, SubdivisionModel>().ReverseMap();
        CreateMap<SubdivisionModel, SelectionViewModel>().ReverseMap();
        CreateMap<SelectionViewModel, Subdivision>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        
        CreateMap<SelectionCreateModel, ProjectTypeModel>();
        CreateMap<ProjectType, ProjectTypeModel>().ReverseMap();
        CreateMap<ProjectTypeModel, SelectionViewModel>().ReverseMap();
        CreateMap<SelectionViewModel, ProjectType>()
            .ForMember(m => m.Projects, o => o.Ignore())
            .ReverseMap();
        
        CreateMap<AbsenceReason, AbsenceReasonModel>().ReverseMap();
        CreateMap<AbsenceReasonModel, AbsenceReasonViewModel>().ReverseMap();
        CreateMap<AbsenceReasonCreateModel, AbsenceReasonModel>().ReverseMap();
        CreateMap<SelectionViewModel, AbsenceReason>()
            .ForMember(m => m.LeaveRequests, o => o.Ignore())
            .ReverseMap();
        
        
    }
}
