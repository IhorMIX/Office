namespace Office.DAL.Entity;

public class User : BaseEntity
{
    public string Login { get; set; } = null!;
    
    public string Password { get; set; } = null!;  
}