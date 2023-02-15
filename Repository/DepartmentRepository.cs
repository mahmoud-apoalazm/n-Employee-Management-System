using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Repository;

public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
{
    public DepartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .OrderBy(d => d.Name)
        .ToListAsync();

    public async Task<Department?> GetDepartmentAsync(Guid departmentId, bool trackChanges)
    {
        return await FindByCondition(d => d.Id.Equals(departmentId), trackChanges)
                    .SingleOrDefaultAsync();
    }

    public void CreateDepartment(Department department) => Create(department);

    public async Task<IEnumerable<Department>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        return await FindByCondition(x => ids.Contains(x.Id), trackChanges)
             .ToListAsync();
    }

    public void DeleteDepartment(Department department) => Delete(department);
}
