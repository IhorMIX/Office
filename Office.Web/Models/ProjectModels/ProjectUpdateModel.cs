namespace Office.Web.Models;

public class ProjectUpdateModel
{
    public int Id { get; set; }
    public int ProjectManagerId { get; set; }
    public int ProjectTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Comment { get; set; } = null!;
    public bool Status { get; set; }
}