namespace Office.Web.Models;

public class AssignEmployeeInTaskModel
{
    public int TaskId { get; set; }
    public List<int> EmployeeIds { get; set; } = null!;
}