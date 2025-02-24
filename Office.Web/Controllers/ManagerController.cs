using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office.BLL.Exceptions;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.Web.Extensions;
using Office.Web.Models;

namespace Office.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ManagerController(IManagerService managerService,IEmployeeService employeeService, IMapper mapper) : ControllerBase
{
    [HttpGet("{managerId:int}")]
    public async Task<IActionResult> GetById(int managerId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var manager = await managerService.GetByIdAsync(managerId, cancellationToken);
        return Ok(mapper.Map<ManagerDetailViewModel>(manager));
    }
    
    [HttpPost("project-manager")]
    public async Task<IActionResult> CreateProjectManager([FromBody] ManagerCreateModel manager,
        CancellationToken cancellationToken)
    {
        var adminId = User.GetUserId();
        var managerResult = await managerService.CreateManagerAsync(adminId,
            mapper.Map<ProjectManagerModel>(manager), cancellationToken);
        return Ok(mapper.Map<ManagerViewModel>(managerResult));
    }
    
    [HttpPost("hr-manager")]
    public async Task<IActionResult> CreateHrManager([FromBody] ManagerCreateModel manager,
        CancellationToken cancellationToken)
    {
        var adminId = User.GetUserId();
        var managerResult = await managerService.CreateManagerAsync(adminId,
            mapper.Map<HrManagerModel>(manager), cancellationToken);
        return Ok(mapper.Map<ManagerViewModel>(managerResult));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateManager([FromBody] ManagerUpdateModel manager, CancellationToken cancellationToken)
    {
        var managerId = User.GetUserId();
        var updatedManager = await managerService.UpdateManagerAsync(managerId,mapper.Map<BaseManagerModel>(manager), cancellationToken);
        return Ok(mapper.Map<ManagerViewModel>(updatedManager));
    }
    
    [HttpDelete("{managerId:int}")]
    public async Task<IActionResult> DeleteManager(int managerId, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        await managerService.DeleteManagerAsync(adminId, managerId, cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllManagers(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var managers = await managerService.GetAll(userId, cancellationToken);
        return Ok(mapper.Map<List<ManagerViewModel>>(managers));
    }
    
    [HttpGet("hr-managers")]
    public async Task<IActionResult> GetHrManagers(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var managers = await managerService.GetHrManagers(userId, cancellationToken);
        return Ok(mapper.Map<List<HrManagerViewModel>>(managers));
    }
    
    [HttpGet("project-managers")]
    public async Task<IActionResult> GetProjectManagers(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var managers = await managerService.GetProjectManagers(userId, cancellationToken);
        return Ok(mapper.Map<List<ProjectManagerViewModel>>(managers));
    }
}