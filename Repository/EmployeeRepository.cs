using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System.Numerics;


namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid departmentId, EmployeeParameters playerParameters, bool trackChanges)
        {
            var employees = await FindByCondition(
            e => e.DepartmentId.Equals(departmentId), trackChanges)
             .FilterPlayers(playerParameters.MinAge, playerParameters.MaxAge)
             .Search(playerParameters.SearchTerm!)
             .Sort(playerParameters.OrderBy!)
             .ToListAsync();

            return PagedList<Employee>.ToPagedList(employees,
             playerParameters.PageNumber, playerParameters.PageSize);
        }
        public async Task<Employee?> GetEmployeeAsync(Guid departmentId, Guid employeeId, bool trackChanges)
        {
            return await FindByCondition(e => e.DepartmentId.Equals(departmentId) && e.Id.Equals(employeeId), trackChanges)
                 .SingleOrDefaultAsync();
        }
        public void CreateEmployeeForDepartment(Guid departmentId, Employee employee)
        {
            employee.DepartmentId = departmentId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);

        


    }
}
