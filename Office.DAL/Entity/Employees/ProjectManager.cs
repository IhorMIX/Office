namespace Office.DAL.Entity;

public class ProjectManager : BaseEmployee
{
    public int CurrentProjectsCount { get; set; }
    public string TeamName { get; set; }
}