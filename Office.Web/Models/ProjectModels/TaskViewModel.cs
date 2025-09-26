using TaskStatus = Office.DAL.Entity.Enums.TaskStatus;

namespace Office.Web.Models;

public class TaskViewModel
{
    public int EmployeeId { get; set; }
    public EmployeeViewModel Employee { get; set; } = null!;

    public int? ProjectId { get; set; }
    public ProjectViewModel? Project { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public string Title { get; set; }  = null!;
    public string Description { get; set; }  = null!;

    public TaskStatus TaskStatus { get; set; }
}