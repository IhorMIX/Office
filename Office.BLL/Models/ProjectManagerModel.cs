namespace Office.BLL.Models;

public class ProjectManagerModel : BaseModel
{
    public ICollection<ProjectModel> Projects { get; set; } = null!;
}