using Office.BLL.Models;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;

namespace Office.Web.Controllers.MapperConfiguration;

public class MapperModelsConfig : AutoMapper.Profile
{
    public MapperModelsConfig()
    {
        CreateMap<BaseEmployee, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<EmployeeModel, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<BaseManager, BaseManagerModel>()
            .ReverseMap();
        
        CreateMap<Employee, EmployeeModel>()
            .ReverseMap();
        
        CreateMap<HrManager, HrManagerModel>()
            .ReverseMap();
        
        CreateMap<ProjectManager, ProjectManagerModel>()
            .ReverseMap();
    }
}