namespace Office.BLL.Models;

public class ProjectManagerModel : BaseManagerModel
{
    public ICollection<ProjectModel> Projects { get; set; } = null!;
}