using Office.DAL.Entity.Employees;

namespace Office.DAL.Entity.Selections;

public class Subdivision : BaseEntity
{
    public string Name { get; set; } = null!;
    public IEnumerable<Employee> Employees { get; set; }= null!;
}