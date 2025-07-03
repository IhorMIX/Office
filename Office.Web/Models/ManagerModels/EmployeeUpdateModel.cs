namespace Office.Web.Models;

public class EmployeeUpdateModel
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public string FullName { get; set; } = null!;
    
    public int SubdivisionId { get; set; }
    
    public int PositionId { get; set; }
    
    public bool Status { get; set; }
    
    public int OutOfOfficeBalance { get; set; }
    public int HrManagerId { get; set; }
}