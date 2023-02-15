
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record DepartmentForManipulationDto
    {

        public string Name { get; init; } = string.Empty;
        public string Location { get; init; } = string.Empty;

        public IEnumerable<EmployeeForCreationDto>? Employees { get; init; } 
    }
}
