using Office.DAL.Entity.Enums;

namespace Office.Web.Models;

public class LeaveRequestViewModel
{
    public int Id { get; set; }
    public BaseEmployeeViewModel Employee { get; set; } = null;
    public AbsenceReasonViewModel AbsenceReason { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveRequestStatus Status { get; set; }
    public string Comment { get; set; } = null!;
}
