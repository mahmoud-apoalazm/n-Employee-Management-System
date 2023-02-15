
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Dynamic;


namespace Service.Contracts;

public interface IManagerService
{
    public Task<(IEnumerable<ExpandoObject> managers, MetaData metaData)> GetManagersAsync(Guid departmentId, ManagerParameters employeeParameters, bool trackChanges);
    public Task<ManagerDto?> GetManagerAsync(Guid departmentId, Guid managerId, bool trackChanges);
    public Task<ManagerDto> CreateManagerForDepartmentAsync(Guid departmentId, ManagerForCreationDto
           managerForCreationDto, bool trackChanges);
    Task DeleteManagerForDepartmentAsync(Guid departmentId, Guid employeeId, bool trackChanges);

    public Task UpdateManagerForDepartmentAsync(Guid departmentId, Guid employeeId, ManagerForUpdateDto
       managerForUpdateDto, bool departmentTrackChanges, bool managerTrackChanges);
    Task<(ManagerForUpdateDto managerToPatch, Manager managerEntity)> GetManagerForPatchAsync(
    Guid departmentId, Guid managerId, bool departmentTrackChanges, bool managerTrackChanges);
    Task SaveChangesForPatchAsync(ManagerForUpdateDto managerToPatch, Manager managerEntity);
}
