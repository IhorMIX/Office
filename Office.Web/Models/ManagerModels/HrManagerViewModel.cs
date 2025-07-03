namespace Office.Web.Models;

public class HrManagerViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public List<EmployeeViewModel> Workers { get; set; } = null!;
}