

using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IManagerService> _managerService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IDataShaper<PersonDto> dataShaper, UserManager<User> userManager,
           IConfiguration configuration)
        {
            _departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repositoryManager, logger, mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, logger, mapper, dataShaper));
            _managerService = new Lazy<IManagerService>(() => new ManagerService(repositoryManager, logger, mapper, dataShaper));

            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationService(logger, mapper, userManager,configuration));


        }

         public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IDepartmentService DepartmentService => _departmentService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;
        public IManagerService ManagerService => _managerService.Value;
    }
}
