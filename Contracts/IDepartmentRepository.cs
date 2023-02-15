

using Entities.Models;

namespace Contracts;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges);
    Task<Department?> GetDepartmentAsync(Guid departmentId, bool trackChanges);
   
    Task<IEnumerable<Department>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void CreateDepartment(Department department);
    
    public void DeleteDepartment(Department team);
}
