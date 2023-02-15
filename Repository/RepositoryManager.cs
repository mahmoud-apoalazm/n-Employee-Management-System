using Contracts;


namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IDepartmentRepository> _departmentRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    private readonly Lazy<IManagerRepository> _managerRepository;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _departmentRepository = new Lazy<IDepartmentRepository>(() => new
       DepartmentRepository(repositoryContext));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new
       EmployeeRepository(repositoryContext));
        _managerRepository = new Lazy<IManagerRepository>(() => new
       ManagerRepository(repositoryContext));
    }

    public IDepartmentRepository Department => _departmentRepository.Value;

    public IEmployeeRepository Employee => _employeeRepository.Value;
    public IManagerRepository Manager => _managerRepository.Value;
    public async Task SaveAsync() =>await _repositoryContext.SaveChangesAsync();


}
