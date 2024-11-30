using Office.DAL.Entity.Enums;

namespace Office.DAL.Entity;

public class BaseEmployee : BaseEntity
{
    public string Login { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
    public string FullName { get; set; } = null!;
    
    public AuthorizationInfo? AuthorizationInfo { get; set; }
    
}