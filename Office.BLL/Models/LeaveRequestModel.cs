using Office.BLL.Models.Enums;

namespace Office.BLL.Models;

public class LeaveRequestModel : BaseModel
{
    public int EmployeeId { get; set; }
    public EmployeeModel Employee { get; set; } = null!;
    
    public int AbsenceReasonId { get; set; }
    public AbsenceReasonModel AbsenceReason { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public LeaveRequestStatus Status { get; set; }
    
    public string Comment { get; set; } = null!;

    public ApprovalRequestModel ApprovalRequest { get; set; } = null!;
}