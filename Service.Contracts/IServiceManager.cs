﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts;

public interface IServiceManager
{
    IDepartmentService DepartmentService { get; }
    IEmployeeService EmployeeService { get; }
    IManagerService ManagerService { get; }
    IAuthenticationService AuthenticationService { get; }

}
