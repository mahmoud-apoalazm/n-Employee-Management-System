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
    internal sealed class ManagerService : IManagerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<PersonDto> _dataShaper;

        public ManagerService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<PersonDto> dataShaper)
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

        private async Task<Manager> GetManagerForDepartmentAndCheckIfItExists
       (Guid departmentId, Guid managerId, bool trackChanges)
        {
            var managerDb = await _repository.Manager.GetManagerAsync(departmentId, managerId, trackChanges);
            if (managerDb is null)
                throw new ManagerNotFoundException(managerId);
            return managerDb;
        }
        //------------------------------------------------------------------------------
        public async Task<(IEnumerable<ExpandoObject> managers, MetaData metaData)> GetManagersAsync(Guid departmentId, ManagerParameters managerParameters, bool trackChanges)
        {
            if (!managerParameters.ValidAgeRange)
                throw new MaxAgeRangeBadRequestException();

            var department = await CheckIfDepartmentExists(departmentId, false);

            var managersWithMetaData = await _repository.Manager.GetManagersAsync(departmentId, managerParameters, trackChanges);
            var managersDto = _mapper.Map<IEnumerable<ManagerDto>>(managersWithMetaData);
            var shapedData = _dataShaper.ShapeData(managersDto,
            managerParameters.Fields!);
            return (managers: shapedData, metaData: managersWithMetaData.MetaData);
        }

        public async Task<ManagerDto?> GetManagerAsync(Guid departmentId, Guid managerId, bool trackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, false);
            var managerFromDb = await GetManagerForDepartmentAndCheckIfItExists(departmentId, managerId, trackChanges);
            var employeeDto = _mapper.Map<ManagerDto>(managerFromDb);
            return employeeDto;
        }

        public async Task<ManagerDto> CreateManagerForDepartmentAsync(Guid departmentId, ManagerForCreationDto
           managerForCreationDto, bool trackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, trackChanges);
            var managerEntity = _mapper.Map<Manager>(managerForCreationDto);
            _repository.Manager.CreateManagerForDepartment(departmentId, managerEntity);
            await _repository.SaveAsync();
            var managerToReturn = _mapper.Map<ManagerDto>(managerEntity);
            return managerToReturn;
        }

        public async Task DeleteManagerForDepartmentAsync(Guid departmentId, Guid managerId, bool trackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, trackChanges);
            var managerForDepartment = await GetManagerForDepartmentAndCheckIfItExists(departmentId, managerId, trackChanges);
            _repository.Manager.DeleteManager(managerForDepartment);
            await _repository.SaveAsync();

        }

        public async Task UpdateManagerForDepartmentAsync(Guid departmentId, Guid managerId, ManagerForUpdateDto
       managerForUpdateDto, bool departmentTrackChanges, bool employeeTrackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, departmentTrackChanges);
            var managerForDepartment = await GetManagerForDepartmentAndCheckIfItExists(departmentId, managerId, employeeTrackChanges);
            _mapper.Map(managerForUpdateDto, managerForDepartment);
            await _repository.SaveAsync();
        }

        public async Task<(ManagerForUpdateDto managerToPatch, Manager managerEntity)> GetManagerForPatchAsync(
             Guid departmentId, Guid managerId, bool departmentTrackChanges, bool managerTrackChanges)
        {
            var department = await CheckIfDepartmentExists(departmentId, departmentTrackChanges);
            var managerEntity = await GetManagerForDepartmentAndCheckIfItExists(departmentId, managerId, managerTrackChanges);
            var managerToPatch = _mapper.Map<ManagerForUpdateDto>(managerEntity);
            return (managerToPatch, managerEntity);
        }

        public async Task SaveChangesForPatchAsync(ManagerForUpdateDto managerToPatch, Manager managerEntity)
        {
            _mapper.Map(managerToPatch, managerEntity);
            await _repository.SaveAsync();
        }

     

        //------------------------------------------------------------------------------

    }
}
