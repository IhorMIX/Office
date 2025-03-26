namespace Office.Web.Models;

public class SubdivisionViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<EmployeeViewModel> Employees { get; set; }= null!;
}