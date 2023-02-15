
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
    [Route("api/departments/{departmentId}/managers")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ManagersController : ControllerBase
    {
        private readonly IServiceManager _service;
        public ManagersController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetManagersForDepartment(Guid departmentId, [FromQuery] ManagerParameters managerParameters)
        {
            var pagedResult = await _service.ManagerService.GetManagersAsync(departmentId, managerParameters, false);
            Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.managers);
        }

        [HttpGet("{managerId:guid}", Name = "GetManagerForDepartment")]
        public async Task<IActionResult> GetEmployeeForTeam(Guid departmentId, Guid managerId)
        {
            var manager = await _service.ManagerService.GetManagerAsync(departmentId, managerId, false);
            return Ok(manager);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateManager(Guid departmentId, [FromBody] ManagerForCreationDto manager)
        {
            var createdManager = await _service.ManagerService.CreateManagerForDepartmentAsync(departmentId, manager, false);
            return CreatedAtRoute("GetManagerForDepartment", new { departmentId = departmentId, managerId = createdManager.Id },
            createdManager);
        }


        [HttpDelete("{managerId:guid}")]
        public async Task<IActionResult> DeleteManagerForDepartment(Guid departmentId, Guid managerId)
        {
            await _service.ManagerService.DeleteManagerForDepartmentAsync(departmentId, managerId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{managerId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateManagerForDepartment(Guid departmentId, Guid managerId,
         [FromBody] ManagerForUpdateDto manager)
        {

            await _service.ManagerService.UpdateManagerForDepartmentAsync(departmentId, managerId, manager,
              departmentTrackChanges: false, managerTrackChanges: true);
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
