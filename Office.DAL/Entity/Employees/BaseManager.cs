namespace Office.DAL.Entity.Employees;

public class BaseManager : BaseEmployee
{
    public ICollection<ApprovalRequest> ApprovalRequests { get; set; } = null!;
}