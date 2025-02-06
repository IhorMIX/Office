using Office.DAL.Entity.Employees;

namespace Office.DAL.Entity;

public class ProjectManager : BaseManager
{
    public ICollection<Project> Projects { get; set; } = null!;
}