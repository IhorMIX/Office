using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Enums;
using Office.DAL.Entity.Selections;

namespace Office.DAL.Entity;

public class LeaveRequest : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public int AbsenceReasonId { get; set; }
    public AbsenceReason AbsenceReason { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public LeaveRequestStatus Status { get; set; }
    
    public string Comment { get; set; } = null!;

    public ApprovalRequest ApprovalRequest { get; set; } = null!;
}