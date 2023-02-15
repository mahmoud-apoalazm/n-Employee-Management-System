

using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(bool trackChanges);
    Task<DepartmentDto?> GetDepartmentAsync(Guid DepartmentId, bool trackChanges);
    Task<DepartmentDto> CreateDepartmentAsync(DepartmentForCreationDto department);
    Task<IEnumerable<DepartmentDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<(IEnumerable<DepartmentDto> departments, string ids)> CreateDepartmentCollection(IEnumerable<DepartmentForCreationDto> departmentCollection);
    Task DeleteDepartmentAsync(Guid DepartmentId,  bool trackChanges);
    Task UpdateDepartmentAsync(Guid DepartmentId, DepartmentForUpdateDto departmentForUpdateDto, bool trackChanges);
}
