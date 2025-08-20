using Office.BLL.Models.Enums;

namespace Office.Web.Models;

public class LeaveRequestUpdateModel
{
    public int Id { get; set; }
    public int AbsenceReasonId { get; set; }
    public int ApprovalRequestId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveRequestStatus Status { get; set; }
    public string Comment { get; set; } = null!;
}