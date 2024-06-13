namespace Office.DAL.Entity;

public class Employee : BaseUser
{
    public string position { get; set; }
    public int PeoplePartnerID { get; set; }
    public int OutOfOfficeBalance  { get; set; }
    
}