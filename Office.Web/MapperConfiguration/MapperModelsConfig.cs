using AutoMapper;
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
        CreateMap<BaseEmployee, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<EmployeeModel, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<Admin, AdminModel>()
            .ReverseMap();
        
        CreateMap<BaseManager, BaseManagerModel>()
            .ReverseMap();
        
        CreateMap<Employee, EmployeeModel>()
            .ReverseMap();
        
        CreateMap<HrManager, HrManagerModel>()
            .ReverseMap();
        
        CreateMap<HrManagerViewModel, HrManagerModel>()
            .ReverseMap();
        
        CreateMap<ProjectManager, ProjectManagerModel>()
            .ReverseMap();
        
        CreateMap<ProjectManagerViewModel, ProjectManagerModel>()
            .ReverseMap();
        
        CreateMap<AuthorizationInfoModel, AuthorizationInfo>()
            .ForMember(m => m.EmployeeId, o => o.Ignore())
            .ForMember(m => m.Employee, o => o.Ignore())
            .ReverseMap();
        
        CreateMap<ApprovalRequest, ApprovalRequestModel>()
            .ReverseMap();  
        CreateMap<ApprovalRequestViewModel, ApprovalRequestModel>()
            .ReverseMap();  
        CreateMap<ApprovalRequestUpdateModel, ApprovalRequestModel>()
            .ReverseMap();  
        CreateMap<AbsenceReasonViewModel, AbsenceReason>()
            .ReverseMap();
        CreateMap<AbsenceReason, AbsenceReasonModel>().ReverseMap();
        CreateMap<AbsenceReasonModel, AbsenceReasonViewModel>()
            .ReverseMap();

        CreateMap<LeaveRequest, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestModel, LeaveRequestViewModel>()
            .ReverseMap();
        CreateMap<LeaveRequestFullViewModel, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestCreateModel, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestUpdateModel, LeaveRequestModel>()
            .ReverseMap();
        
        CreateMap<Project, ProjectModel>()
            .ReverseMap();
        
        CreateMap<Employee, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, ManagerCreateModel>()
            .ReverseMap();
        CreateMap<ProjectManagerModel, ManagerCreateModel>()
            .ReverseMap();
        CreateMap<HrManagerModel, ManagerCreateModel>()
            .ReverseMap();
        
        CreateMap<Employee, BaseEmployee>()
            .ReverseMap();
        
        CreateMap<EmployeeModel, BaseEmployee>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, ProjectManager>()
            .ReverseMap();
        CreateMap<BaseManagerModel, HrManager>()
            .ReverseMap();
        CreateMap<BaseManager, ProjectManager>()
            .ReverseMap();
        CreateMap<BaseManagerModel, ManagerUpdateModel>()
            .ReverseMap();
        CreateMap<BaseManagerModel, ManagerViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();
        CreateMap<BaseManager, ManagerViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();
        
        CreateMap<HrManagerModel, ManagerViewModel>()
            .ReverseMap();
        
        CreateMap<SelectionViewModel, Position>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        CreateMap<SelectionViewModel, AbsenceReason>()
            .ForMember(m => m.LeaveRequests, o => o.Ignore())
            .ReverseMap();
        CreateMap<SelectionViewModel, Subdivision>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        CreateMap<SelectionViewModel, ProjectType>()
            .ForMember(m => m.Projects, o => o.Ignore())
            .ReverseMap();
        
        CreateMap<SelectionCreateModel, Subdivision>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore());
        CreateMap<SelectionCreateModel, Position>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore());
        CreateMap<SelectionCreateModel, ProjectType>()
            .ForMember(dest => dest.Projects, opt => opt.Ignore());
        CreateMap<SelectionCreateModel, AbsenceReason>()
            .ForMember(dest => dest.LeaveRequests, opt => opt.Ignore());
        
        CreateMap<SelectionViewModel, SelectionModel>()
            .ReverseMap();
        CreateMap<SelectionViewModel, SelectionModel>()
            .ReverseMap();
        CreateMap<SelectionViewModel, SelectionModel>()
            .ReverseMap();
        
        CreateMap<SelectionModel, Position>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        CreateMap<SelectionModel, Subdivision>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        CreateMap<SelectionModel, ProjectType>()
            .ForMember(m => m.Projects, o => o.Ignore())
            .ReverseMap();
        
        
        CreateMap<EmployeeViewModel, EmployeeModel>()
            .ReverseMap();
        CreateMap<EmployeeViewModel, Employee>()
            .ReverseMap();
        CreateMap<EmployeeFullViewModel, EmployeeModel>()
            .ReverseMap();
        CreateMap<BaseEmployeeViewModel, EmployeeModel>()
            .ReverseMap();
        CreateMap<EmployeeCreateModel, EmployeeModel>()
            .ReverseMap();
        CreateMap<EmployeeUpdateModel, EmployeeModel>()
            .ReverseMap();
        
        CreateMap<ProjectViewModel, Project>()
            .ReverseMap();
        CreateMap<ProjectViewModel, ProjectModel>()
            .ReverseMap();
        CreateMap<ProjectCreateModel, ProjectModel>()
            .ReverseMap();
        CreateMap<ProjectUpdateModel, ProjectModel>()
            .ReverseMap();

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
        CreateMap<ProjectManager, ManagerDetailViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();
    }
}
