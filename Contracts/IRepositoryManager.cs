

namespace Contracts;

public interface IRepositoryManager
{
    IDepartmentRepository Department { get; }
    IEmployeeRepository Employee { get; }
    IManagerRepository Manager { get; }
    Task SaveAsync();
}
