using Office.DAL.Entity.Employees;
using TaskStatus = Office.DAL.Entity.Enums.TaskStatus;

namespace Office.DAL.Entity;

public class TaskEntity : BaseEntity
{
    public ICollection<Employee> Employees { get; set; } = null!;
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public string Title { get; set; }  = null!;
    public string Description { get; set; }  = null!;

    public TaskStatus TaskStatus { get; set; }
}
