
using EmployeeManagementSystem.Presentation.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

using System.Text.Json;

namespace EmployeeManagementSystem.Presentation.Controllers
{
    [Route("api/departments/{departmentId}/employees")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetEmployeesForDepartment(Guid departmentId, [FromQuery] EmployeeParameters employeeParameters)
        {
            var pagedResult = await _service.EmployeeService.GetEmployeesAsync(departmentId, employeeParameters, false);
            Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.employees);
        }

        [HttpGet("{employeeId:guid}", Name = "GetEmployeeForDepartment")]
        public async Task<IActionResult> GetEmployeeForTeam(Guid departmentId, Guid employeeId)
        {
            var employee = await _service.EmployeeService.GetEmployeeAsync(departmentId, employeeId, false);
            return Ok(employee);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployee(Guid departmentId, [FromBody] EmployeeForCreationDto employeer)
        {
            var createdEmployee = await _service.EmployeeService.CreateEmployeeForDepartmentAsync(departmentId, employeer, false);
            return CreatedAtRoute("GetEmployeeForDepartment", new { departmentId = departmentId, employeeId = createdEmployee.Id },
            createdEmployee);
        }


        [HttpDelete("{employeeId:guid}")]
        public async Task<IActionResult> DeleteEmployeeForDepartment(Guid departmentId, Guid employeeId)
        {
            await _service.EmployeeService.DeleteEmployeeForDepartmentAsync(departmentId, employeeId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{employeeId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEmployeeForDepartment(Guid departmentId, Guid employeeId,
         [FromBody] EmployeeForUpdateDto employee)
        {

            await _service.EmployeeService.UpdateEmployeeForDepartmentAsync(departmentId, employeeId, employee,
              departmentTrackChanges: false, employeeTrackChanges: true);
            return NoContent();
        }


        //[HttpPatch("{playerId:guid}")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //public async Task<IActionResult> PartiallyUpdatePlayerForTeam(Guid teamId, Guid playerId,
        //    [FromBody] JsonPatchDocument<playerForUpdateDto> patchDoc)
        //{
        //    var result = await _service.PlayerService.GetPlayerForPatchAsync(teamId, playerId,
        //     false,
        //     true);
        //    patchDoc.ApplyTo(result.playerToPatch);
        //    TryValidateModel(result.playerToPatch);

        //    await _service.PlayerService.SaveChangesForPatchAsync(result.playerToPatch,
        //    result.playerEntity);
        //    return NoContent();
        //}
    }
}
