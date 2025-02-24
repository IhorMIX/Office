namespace Office.BLL.Models;

public class HrManagerModel : BaseManagerModel
{
    public ICollection<EmployeeModel> Workers { get; set; } = null!;
}