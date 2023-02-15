using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IManagerRepository
{
    Task<PagedList<Manager>> GetManagersAsync(Guid departmentId, ManagerParameters managerParameters, bool trackChanges);
    Task<Manager?> GetManagerAsync(Guid departmentId, Guid managerId, bool trackChanges);
    void CreateManagerForDepartment(Guid departmentId, Manager manager);
    void DeleteManager(Manager manager);

}
