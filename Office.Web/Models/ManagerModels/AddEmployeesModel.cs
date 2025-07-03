namespace Office.Web.Models;

public class AddEmployeesModel
{
    public int ProjectId { get; set; }
    public List<int> EmployeesIds { get; set; } = null!;
}