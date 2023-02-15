using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<PersonDto> _dataShaper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<PersonDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }
        private async Task<Department> CheckIfDepartmentExists(Guid departmentId, bool trackChanges)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);
            return department;
        }

        private async Task<Employee> GetEmployeeForDepartmentAndCheckIfItExists
       (Guid departmentId, Guid employeeId, bool trackChanges)
        {
            var employeeDb = await _repository.Employee.GetEmployeeAsync(departmentId, employeeId, trackChanges);
            if (employeeDb is null)
                throw new EmployeeNotFoundException(employeeId);
            return employeeDb;
        }
        //------------------------------------------------------------------------------
        public async Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid departmentId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            if (!employeeParameters.ValidAgeRange)
                throw new MaxAgeRangeBadRequestException();

            var department = await CheckIfDepartmentExists(departmentId, false);

            var employeesWithMetaData = await _repository.Employee.GetEmployeesAsync(departmentId, employeeParameters, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
            var shapedData = _dataShaper.ShapeData(employeesDto,
            employeeParameters.Fields!);
            return (emplyees: shapedData, metaData: employeesWithMetaData.MetaData);
        }

        public async Task<EmployeeDto?> GetEmployeeAsync(Guid departmentId, Guid employeeId, bool trackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, false);
            var employeeFromDb = await GetEmployeeForDepartmentAndCheckIfItExists(departmentId, employeeId, trackChanges);
            var employeeDto = _mapper.Map<EmployeeDto>(employeeFromDb);
            return employeeDto;
        }

        public async Task<EmployeeDto> CreateEmployeeForDepartmentAsync(Guid departmentId, EmployeeForCreationDto
           employeeForCreationDto, bool trackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, trackChanges);
            var employeeEntity = _mapper.Map<Employee>(employeeForCreationDto);
            _repository.Employee.CreateEmployeeForDepartment(departmentId, employeeEntity);
            await _repository.SaveAsync();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }

        public async Task DeleteEmployeeForDepartmentAsync(Guid departmentId, Guid employeeId, bool trackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, trackChanges);
            var employeeForDepartment = await GetEmployeeForDepartmentAndCheckIfItExists(departmentId, employeeId, trackChanges);
            _repository.Employee.DeleteEmployee(employeeForDepartment);
            await _repository.SaveAsync();

        }

        public async Task UpdateEmployeeForDepartmentAsync(Guid departmentId, Guid employeeId, EmployeeForUpdateDto
       employeeForUpdateDto, bool departmentTrackChanges, bool employeeTrackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, departmentTrackChanges);
            var employeeForDepartment = await GetEmployeeForDepartmentAndCheckIfItExists(departmentId, employeeId, employeeTrackChanges);
            _mapper.Map(employeeForUpdateDto, employeeForDepartment);
            await _repository.SaveAsync();
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
             Guid departmentId, Guid employeeId, bool departmentTrackChanges, bool employeeTrackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, departmentTrackChanges);
            var employeeEntity = await GetEmployeeForDepartmentAndCheckIfItExists(departmentId, employeeId, employeeTrackChanges);
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
            return (employeeToPatch, employeeEntity);
        }

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto EmployeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(EmployeeToPatch, employeeEntity);
            await _repository.SaveAsync();
        }

        //------------------------------------------------------------------------------

    }
}
