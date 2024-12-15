using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Enums;

namespace Office.DAL.Entity;

public class ApprovalRequest : BaseEntity
{
    public int ApproverId { get; set; }
    public BaseManager Approver { get; set; } = null!;
    
    public int LeaveRequestId { get; set; }
    public LeaveRequest LeaveRequest { get; set; } = null!;

    public ApprovalRequestStatus Status { get; set; }
    
    public string Comment { get; set; } = null!;
}