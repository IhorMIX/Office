namespace Office.Web.Models;

public class TaskCreateModel
{
    public int? EmployeeId { get; set; }

    public int? ProjectId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public string Title { get; set; }  = null!;
    public string Description { get; set; }  = null!;
}