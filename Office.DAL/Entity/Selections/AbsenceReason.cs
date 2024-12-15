namespace Office.DAL.Entity.Selections;

public class AbsenceReason  : BaseEntity
{
    public string ReasonDescription { get; set; } = null!;
    public IEnumerable<LeaveRequest> LeaveRequests { get; set; } = null!;
}