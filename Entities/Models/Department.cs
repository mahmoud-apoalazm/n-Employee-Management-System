
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Entities.Models;

public class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<Manager> Managers { get; set; } = new List<Manager>();
}
