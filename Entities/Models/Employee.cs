

using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Employee :Person
{
    public Guid? DepartmentId { get; set; }
    public Department? Department { get; set; }


    public Guid? ManagerId { get; set; }

    public Manager? Manager { get; set; }
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public List<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

}
