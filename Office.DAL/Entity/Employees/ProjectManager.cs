namespace Office.DAL.Entity;

public class ProjectManager : BaseEmployee
{
    public ICollection<Project> Projects { get; set; } = null!;
}