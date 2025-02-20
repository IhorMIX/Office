using Office.Web.Models.Enums;

namespace Office.Web.Models;

public class CurrentUserViewModel
{
    public string FullName { get; set; } = null!;
    public EmployeeType EmployeeType { get; set; }
}