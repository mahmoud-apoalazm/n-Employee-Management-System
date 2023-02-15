
using Entities.Models;

using System.Linq.Dynamic.Core;
namespace Repository.Extensions.Utility
{
    public static class RepositoryManagerExtensions
    {
        public static IQueryable<Manager> FilterManagers(this IQueryable<Manager>
           managers, uint minAge, uint maxAge) =>
             managers.Where(e => ((DateTime.Now.Year) - (e.DateOfBirth.Year)) >= minAge && ((DateTime.Now.Year) - (e.DateOfBirth.Year)) <= maxAge);
        //---------------------------------------------------------------------
        public static IQueryable<Manager> Search(this IQueryable<Manager> managers,
       string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return managers;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return managers.Where(p => p.Name.ToLower().Contains(lowerCaseTerm));
        }
        //------------------------------------------------------------------------

        public static IQueryable<Manager> Sort(this IQueryable<Manager> managers, string
           orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return managers.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Manager>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return managers.OrderBy(e => e.Name);
            return managers.OrderBy(orderQuery);
        }
    }
}
