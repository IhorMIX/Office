namespace Office.Web.Models;

public class AuthorizeModel
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsNeedToRemember { get; set; }
}