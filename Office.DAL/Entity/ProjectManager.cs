namespace Office.DAL.Entity;

public class ProjectManager : BaseUser
{
    public int CurrentProjectsCount { get; set; }
    public string TeamName { get; set; }
}