namespace Office.Web.Models;

public class ProjectViewModel
{
    public int Id { get; set; }
    public ManagerViewModel ProjectManager { get; set; } = null!;
    public SelectionViewModel ProjectType { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Comment { get; set; } = null!;
    public bool Status { get; set; }
    public List<EmployeeViewModel> Employees { get; set; } = null!;
}