
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class DepartmentService :IDepartmentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public DepartmentService(IRepositoryManager repository, ILoggerManager logger,IMapper mapper)
        {
            _repository= repository;
            _logger= logger;
            _mapper = mapper;
        }

        private async Task<Department> GetDepartmentAndCheckIfItExists(Guid departmentId, bool trackChanges)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);
            return department;
        }

        //------------------------------------------------------------------------------

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(bool trackChanges)
        {
           var departments= await _repository.Department.GetAllDepartmentsAsync(trackChanges); 
            var departmentDto = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return departmentDto;
        }

        //------------------------------------------------------------------------------

        public async Task<DepartmentDto?> GetDepartmentAsync(Guid departmentId, bool trackChanges)
        {
            var department = await GetDepartmentAndCheckIfItExists(departmentId, trackChanges);
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }
        //------------------------------------------------------------------------------

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentForCreationDto department)
        {
            var departmentEntity = _mapper.Map<Department>(department);
            _repository.Department.CreateDepartment(departmentEntity);
            await _repository.SaveAsync();
            var departmentToReturn = _mapper.Map<DepartmentDto>(departmentEntity);
            return departmentToReturn;
        }
        //------------------------------------------------------------------------------

        public async Task<IEnumerable<DepartmentDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
           if(ids is null)
                throw new IdParametersBadRequestException();

           var departmentEntities = await _repository.Department.GetByIdsAsync(ids, trackChanges);   
           if(ids.Count() != departmentEntities.Count())
                throw new CollectionByIdsBadRequestException();
           var departmentsToReturn=_mapper.Map<IEnumerable<DepartmentDto>>(departmentEntities);

            return departmentsToReturn;

        }
        //------------------------------------------------------------------------------

        public async Task<(IEnumerable<DepartmentDto> departments, string ids)> CreateDepartmentCollection(IEnumerable<DepartmentForCreationDto> departmentCollection)
        {
            if (departmentCollection is null)
                throw new DepartmentCollectionBadRequest();
            var departmentEntities = _mapper.Map<IEnumerable<Department>>(departmentCollection);
            foreach (var department in departmentEntities)
            {
                _repository.Department.CreateDepartment(department);
            }
            await _repository.SaveAsync();
            var teamCollectionToReturn = _mapper.Map<IEnumerable<DepartmentDto>>(departmentEntities);
            var ids = string.Join(",", teamCollectionToReturn.Select(t => t.Id));
            return (teamCollectionToReturn, ids);
        }

        public async Task DeleteDepartmentAsync(Guid departmentId, bool trackChanges)
        {
            var department = await GetDepartmentAndCheckIfItExists(departmentId, false);
            _repository.Department.DeleteDepartment(department);
            await _repository.SaveAsync();
        }
        //------------------------------------------------------------------------------

        public async Task UpdateDepartmentAsync(Guid departmentId, DepartmentForUpdateDto departmentForUpdateDto, bool trackChanges)
        {
            var department = await GetDepartmentAndCheckIfItExists(departmentId, trackChanges);
            _mapper.Map(departmentForUpdateDto, department);
            await _repository.SaveAsync();
        }

        //------------------------------------------------------------------------------

    }
}
