
using EmployeeManagementSystem.Presentation.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

using EmployeeManagementSystem.Presentation.ModelBinders;
using System.Data;

namespace EmployeeManagementSystem.Presentation.Controllers
{
    [Route("api/departments")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class DepartmentsController :ControllerBase
    {
        private readonly IServiceManager _service;
        public DepartmentsController(IServiceManager service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _service.DepartmentService.GetAllDepartmentsAsync(false);
            return Ok(departments);
        }


        [HttpGet("{id:guid}", Name = "DepartmentById")]
        //[Authorize(Roles = "Manager")]

        public async Task<IActionResult> GetDepartment(Guid id)
        {
            var department = await _service.DepartmentService.GetDepartmentAsync(id, trackChanges: false);
            return Ok(department);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentForCreationDto department)
        {
            var createdDepartment = await _service.DepartmentService.CreateDepartmentAsync(department);
            return CreatedAtRoute("DepartmentById", new { id = createdDepartment.Id },
            createdDepartment);
        }

        [HttpGet("collection/({ids})", Name = "DepartmentCollection")]
        public async Task<IActionResult> GetDepartmentCollection([ModelBinder(BinderType =
         typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var departmens = await _service.DepartmentService.GetByIdsAsync(ids, trackChanges: false);
            return Ok(departmens);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateDepartmentCollection([FromBody]
         IEnumerable<DepartmentForCreationDto> departmentCollection)
        {
            var result =
            await _service.DepartmentService.CreateDepartmentCollection(departmentCollection);
            return CreatedAtRoute("DepartmentCollection", new { result.ids },
            result.departments);
        }




        [HttpDelete("{departmentId:guid}")]
        public async Task<IActionResult> DeleteDepartment(Guid departmentId)
        {
            await _service.DepartmentService.DeleteDepartmentAsync(departmentId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{departmentId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateTeam(Guid departmentId, [FromBody] DepartmentForUpdateDto department)
        {

            await _service.DepartmentService.UpdateDepartmentAsync(departmentId, department, trackChanges: true);
            return NoContent();
        }

    }
}
