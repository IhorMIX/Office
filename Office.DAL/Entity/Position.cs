namespace Office.DAL.Entity;

public class Position : BaseEntity
{
    public string Name { get; set; } = null!;
    public IEnumerable<Employee> Employees { get; set; }= null!;
}