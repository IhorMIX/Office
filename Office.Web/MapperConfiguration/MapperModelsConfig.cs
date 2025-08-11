using AutoMapper;
using Office.BLL.Models;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Selections;
using Office.Web.Models;

namespace Office.Web.MapperConfiguration;

public class MapperModelsConfig : Profile
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
        CreateMap<BaseEmployeeViewModel, EmployeeModel>().ReverseMap();
        CreateMap<EmployeeUpdateModel, EmployeeModel>().ReverseMap();
        
        CreateMap<BaseManager, BaseManagerModel>().ReverseMap();
        CreateMap<HrManager, HrManagerModel>().ReverseMap();
        CreateMap<ProjectManager, ProjectManagerModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

        CreateMap<BaseManagerModel, ManagerUpdateModel>().ReverseMap();

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

        CreateMap<HrManagerViewModel, HrManagerModel>().ReverseMap();

        CreateMap<ProjectManager, ManagerDetailViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();

        CreateMap<ProjectManagerViewModel, ProjectManagerModel>().ReverseMap();
        
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
        
        CreateMap<SelectionCreateModel, Position>();
        CreateMap<SelectionModel, Position>().ReverseMap();
        CreateMap<Position, SelectionViewModel>().ReverseMap();
        CreateMap<SelectionViewModel, Position>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();

        CreateMap<SelectionCreateModel, Subdivision>();
        CreateMap<SelectionModel, Subdivision>().ReverseMap();
        CreateMap<Subdivision, SelectionViewModel>().ReverseMap();
        CreateMap<SelectionViewModel, Subdivision>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();

        CreateMap<SelectionCreateModel, ProjectType>();
        CreateMap<SelectionModel, ProjectType>().ReverseMap();
        CreateMap<ProjectType, SelectionViewModel>().ReverseMap();
        CreateMap<SelectionViewModel, ProjectType>()
            .ForMember(m => m.Projects, o => o.Ignore())
            .ReverseMap();
        
        CreateMap<SelectionModel, SelectionViewModel>().ReverseMap();
        
        CreateMap<Project, ProjectModel>().ReverseMap();
        CreateMap<ProjectViewModel, Project>().ReverseMap();
        CreateMap<ProjectViewModel, ProjectModel>().ReverseMap();
        CreateMap<ProjectCreateModel, ProjectModel>().ReverseMap();
        CreateMap<ProjectUpdateModel, ProjectModel>().ReverseMap();
        
        CreateMap<ApprovalRequest, ApprovalRequestModel>().ReverseMap();
        CreateMap<ApprovalRequestViewModel, ApprovalRequestModel>().ReverseMap();
        CreateMap<ApprovalRequestUpdateModel, ApprovalRequestModel>().ReverseMap();

        CreateMap<LeaveRequest, LeaveRequestModel>().ReverseMap();
        CreateMap<LeaveRequestModel, LeaveRequestViewModel>().ReverseMap();
        CreateMap<LeaveRequestFullViewModel, LeaveRequestModel>().ReverseMap();
        CreateMap<LeaveRequestCreateModel, LeaveRequestModel>().ReverseMap();
        CreateMap<LeaveRequestUpdateModel, LeaveRequestModel>().ReverseMap();
        
        CreateMap<SelectionModel, SelectionViewModel>().ReverseMap();
        CreateMap<SelectionModel, SelectionViewModel>().ReverseMap();
        CreateMap<SelectionModel, SelectionViewModel>().ReverseMap();
    }
}
