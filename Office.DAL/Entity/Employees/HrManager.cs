using Office.DAL.Entity.Employees;

namespace Office.DAL.Entity;

public class HrManager : BaseManager
{
    public ICollection<Employee> Workers { get; set; } = null!;
}