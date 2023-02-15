using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions.Utility;
using Shared.RequestFeatures;
using System.Numerics;


namespace Repository
{
    public class ManagerRepository : RepositoryBase<Manager>, IManagerRepository
    {
        public ManagerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public async Task<PagedList<Manager>> GetManagersAsync(Guid departmentId, ManagerParameters managerParameters, bool trackChanges)
        {
            var managers = await FindByCondition(
            e => e.DepartmentId.Equals(departmentId), trackChanges)
             .FilterManagers(managerParameters.MinAge, managerParameters.MaxAge)
             .Search(managerParameters.SearchTerm!)
             .Sort(managerParameters.OrderBy!)
             .ToListAsync();

            return PagedList<Manager>.ToPagedList(managers,
             managerParameters.PageNumber, managerParameters.PageSize);
        }
        public async Task<Manager?> GetManagerAsync(Guid departmentId, Guid managerId, bool trackChanges)
        {
            return await FindByCondition(e => e.DepartmentId.Equals(departmentId) && e.Id.Equals(managerId), trackChanges)
                 .SingleOrDefaultAsync();
        }
        public void CreateManagerForDepartment(Guid departmentId, Manager manager)
        {
            manager.DepartmentId = departmentId;
            Create(manager);
        }

        public void DeleteManager(Manager manager) => Delete(manager);

        


    }
}
