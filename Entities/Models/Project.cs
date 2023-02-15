using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class Project
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Status { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public List<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

}
