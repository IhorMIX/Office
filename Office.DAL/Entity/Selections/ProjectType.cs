namespace Office.DAL.Entity.Selections;

public class ProjectType : BaseEntity
{
    public string Name { get; set; } = null!;
    public IEnumerable<Project> Projects { get; set; } = null!;
}