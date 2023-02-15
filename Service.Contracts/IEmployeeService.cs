
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Dynamic;


namespace Service.Contracts;

public interface IEmployeeService
{
    public Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid departmentId, EmployeeParameters employeeParameters, bool trackChanges);
    public Task<EmployeeDto?> GetEmployeeAsync(Guid departmentId, Guid employeeId, bool trackChanges);
    public Task<EmployeeDto> CreateEmployeeForDepartmentAsync(Guid departmentId, EmployeeForCreationDto
           employeeForCreationDto, bool trackChanges);
    Task DeleteEmployeeForDepartmentAsync(Guid departmentId, Guid employeeId, bool trackChanges);

    public Task UpdateEmployeeForDepartmentAsync(Guid departmentId, Guid employeeId, EmployeeForUpdateDto
       employeeForUpdateDto, bool departmentTrackChanges, bool employeeTrackChanges);
    Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
    Guid departmentId, Guid employeeId, bool departmentTrackChanges, bool employeeTrackChanges);
    Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
}
