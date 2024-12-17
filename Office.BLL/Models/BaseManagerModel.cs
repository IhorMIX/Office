namespace Office.BLL.Models;

public class BaseManagerModel : BaseEmployeeModel
{
    public ICollection<ApprovalRequestModel> ApprovalRequest { get; set; } = null!;
}