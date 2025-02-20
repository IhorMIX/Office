using Office.DAL.Entity.Selections;

namespace Office.DAL.Entity.Employees;

public class Employee : BaseEmployee
{
    public Position Position { get; set; } = null!;
    public int PositionId { get; set; }
    
    public Subdivision Subdivision { get; set; } = null!;
    public int SubdivisionId { get; set; }
    
    public bool Status { get; set; }
    
    public int OutOfOfficeBalance  { get; set; }
    public HrManager? HrManager { get; set; }
    public int? HrManagerId { get; set; }
    
    public ICollection<LeaveRequest> LeaveRequests { get; set; } = null!;
    public ICollection<Project> Projects { get; set; } = null!;
}