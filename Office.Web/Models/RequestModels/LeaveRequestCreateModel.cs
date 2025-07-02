namespace Office.Web.Models;

public class LeaveRequestCreateModel
{
    public int ApproverId { get; set; }
    public int AbsenceReasonId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Comment { get; set; } = null!;
}