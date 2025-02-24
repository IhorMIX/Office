namespace Office.Web.Models;

public class EmployeeFullViewModel
{
    public int Id { get; set; }
    
    public string FullName { get; set; } = null!;
    
    public SelectionViewModel Subdivision { get; set; }= null!;
    public SelectionViewModel Position { get; set; }= null!;
    
    public bool Status { get; set; }
    public int OutOfOfficeBalance { get; set; }
    
    public ManagerViewModel HrManager { get; set; } = null!;
    public List<ProjectViewModel> Projects { get; set; } = null!;
}