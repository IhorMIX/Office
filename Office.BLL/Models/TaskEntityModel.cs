using Office.DAL.Entity.Employees;
using TaskStatus = Office.BLL.Models.Enums.TaskStatus;

namespace Office.BLL.Models;

public class TaskEntityModel : BaseModel
{
    public ICollection<Employee> Employees { get; set; } = null!;
    
    public int? ProjectId { get; set; }
    public ProjectModel? Project { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public string Title { get; set; }  = null!;
    public string Description { get; set; }  = null!;

    public TaskStatus TaskStatus { get; set; }
}