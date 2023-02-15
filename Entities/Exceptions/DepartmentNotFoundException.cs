
namespace Entities.Exceptions;

public sealed class DepartmentNotFoundException :NotFoundException
{
    public DepartmentNotFoundException(Guid departmentId)
        : base($"The Department with id: {departmentId} doesn't exist in the database.")

    {

    }
}
