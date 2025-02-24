namespace Office.BLL.Models;

public class EmployeeModel : BaseEmployeeModel
{
    public PositionModel Position { get; set; } = null!;
    public int PositionId { get; set; }
    
    public SubdivisionModel Subdivision { get; set; } = null!;
    public int SubdivisionId { get; set; }
    
    public bool Status { get; set; }
    
    public int OutOfOfficeBalance  { get; set; }
    
    public HrManagerModel? HrManager { get; set; }
    public int? HrManagerId { get; set; }
    
    public ICollection<LeaveRequestModel> LeaveRequests { get; set; } = null!;
    public ICollection<ProjectModel> Projects { get; set; } = null!;
}