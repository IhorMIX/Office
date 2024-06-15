namespace Office.DAL.Entity;

public class ApprovalRequest : BaseEntity
{
    public int ApproverId { get; set; }
    public int LeaveRequestId { get; set; }
    public string Status { get; set; }
    public string Comment { get; set; }
    //if requset was approved
}