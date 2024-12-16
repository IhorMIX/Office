using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Selections;

namespace Office.DAL.Entity;

public class Project : BaseEntity
{
    public int? ProjectManagerId { get; set; }
    public ProjectManager? ProjectManager { get; set; } = null!;
    
    public int ProjectTypeId { get; set; }
    public ProjectType ProjectType { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public string Comment { get; set; } = null!;
    
    public ICollection<Employee> Employees { get; set; } = null!;
    
    public bool Status { get; set; }
}