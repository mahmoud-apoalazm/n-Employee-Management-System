
namespace Entities.Models;

public class Manager :Person
{

    public Guid? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
