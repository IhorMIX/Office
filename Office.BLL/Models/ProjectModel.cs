namespace Office.BLL.Models;

public class ProjectModel : BaseModel
{
    public int? ProjectManagerId { get; set; }
    public ProjectManagerModel? ProjectManager { get; set; } = null!;
    
    public int ProjectTypeId { get; set; }
    public ProjectTypeModel ProjectType { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public string Comment { get; set; } = null!;
    
    public ICollection<EmployeeModel> Employees { get; set; } = null!;
    
    public bool Status { get; set; }
}