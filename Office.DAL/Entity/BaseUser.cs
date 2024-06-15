using Office.DAL.Entity.Enums;

namespace Office.DAL.Entity;

public class BaseUser : BaseEntity
{
    public string Login { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
}