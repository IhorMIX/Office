namespace Office.BLL.Models;

public class BaseEmployeeModel : BaseModel
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public string FullName { get; set; } = null!;

    public int AuthorizationInfoId { get; set; }
    public AuthorizationInfoModel? AuthorizationInfo { get; set; } = null!;
    public bool isDeactivated { get; set; }
}