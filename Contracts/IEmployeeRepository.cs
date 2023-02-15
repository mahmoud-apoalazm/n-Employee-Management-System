
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IEmployeeRepository
{
    Task<PagedList<Employee>> GetEmployeesAsync(Guid departmentId, EmployeeParameters playerParameters, bool trackChanges);
    Task<Employee?> GetEmployeeAsync(Guid departmentId, Guid employeeId, bool trackChanges);
    void CreateEmployeeForDepartment(Guid departmentId, Employee employee);
    void DeleteEmployee(Employee employee);

}
