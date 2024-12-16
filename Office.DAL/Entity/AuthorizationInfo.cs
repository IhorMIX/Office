namespace Office.DAL.Entity;

public class AuthorizationInfo : BaseEntity
{
    public string RefreshToken { get; set; } = null!;

    public DateTime? ExpiredDate { get; set; }

    public int EmployeeId { get; set; }
    public BaseEmployee Employee { get; set; } = null!;
}