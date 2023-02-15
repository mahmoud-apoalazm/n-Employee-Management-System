using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class RepositoryManagerExtensions
    {
        public static IQueryable<Employee> FilterPlayers(this IQueryable<Employee>
           employees, uint minAge, uint maxAge) =>
             employees.Where(e => ((DateTime.Now.Year) - (e.DateOfBirth.Year)) >= minAge && ((DateTime.Now.Year) - (e.DateOfBirth.Year)) <= maxAge);
        //---------------------------------------------------------------------
        public static IQueryable<Employee> Search(this IQueryable<Employee> employees,
       string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return employees;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return employees.Where(p => p.Name.ToLower().Contains(lowerCaseTerm));
        }
        //------------------------------------------------------------------------

        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string
           orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employees.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.Name);
            return employees.OrderBy(orderQuery);
        }
    }
}
