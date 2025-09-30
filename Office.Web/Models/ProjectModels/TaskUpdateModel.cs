using TaskStatus = Office.DAL.Entity.Enums.TaskStatus;

namespace Office.Web.Models;

public class TaskUpdateModel
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int? ProjectId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TaskStatus TaskStatus { get; set; }
    public string Title { get; set; }  = null!;
    public string Description { get; set; }  = null!;
}