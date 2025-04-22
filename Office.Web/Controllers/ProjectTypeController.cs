using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.Web.Extensions;
using Office.Web.Models;

namespace Office.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectTypeController(IProjectTypeService projectTypeService, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{projectTypeId:int}")]
    public async Task<IActionResult> GetById(int projectTypeId, CancellationToken cancellationToken = default)
    {
        var projectType = await projectTypeService.GetByIdAsync(projectTypeId, cancellationToken);
        return Ok(mapper.Map<SelectionViewModel>(projectType));
    }

    [HttpPost("create-projectType")]
    public async Task<IActionResult> CreatePosition(SelectionCreateModel projectTypeCreateModel, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        var result = await projectTypeService.CreateProjectTypeAsync(mapper.Map<ProjectTypeModel>(projectTypeCreateModel), adminId, cancellationToken);
        return Ok(mapper.Map<SelectionViewModel>(result));
    }
    
    [HttpDelete("{projectTypeId:int}")]
    public async Task<IActionResult> DeletePosition(int projectTypeId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await projectTypeService.DeleteProjectTypeAsync(projectTypeId,managerId,cancellationToken);
        return Ok();
    }
}