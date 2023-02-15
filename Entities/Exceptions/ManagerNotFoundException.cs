using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class ManagerNotFoundException : NotFoundException
    {
        public ManagerNotFoundException(Guid managerId)
       : base($"The Manager with id: {managerId} doesn't exist in the database.")

        {

        }
    }
}
