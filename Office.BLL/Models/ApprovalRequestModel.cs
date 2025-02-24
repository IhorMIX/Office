using Office.BLL.Models.Enums;

namespace Office.BLL.Models;

public class ApprovalRequestModel : BaseModel
{
    public int ApproverId { get; set; }
    public BaseManagerModel Approver { get; set; } = null!;
    
    public int LeaveRequestId { get; set; }
    public LeaveRequestModel LeaveRequest { get; set; } = null!;

    public ApprovalRequestStatus Status { get; set; }
    
    public string Comment { get; set; } = null!;
}