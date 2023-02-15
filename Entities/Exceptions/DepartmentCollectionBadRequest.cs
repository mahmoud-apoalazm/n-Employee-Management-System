

namespace Entities.Exceptions
{
    public sealed class DepartmentCollectionBadRequest : BadRequestException
    {
        public DepartmentCollectionBadRequest()
              : base("Department collection sent from a client is null.")
        { 
        }
    }
}
