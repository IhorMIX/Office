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
public class ProjectController(IProjectService projectService, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{projectId:int}")]
    public async Task<IActionResult> GetById(int projectId, CancellationToken cancellationToken = default)
    {
        var project = await projectService.GetByIdAsync(projectId, cancellationToken);
        return Ok(mapper.Map<ProjectViewModel>(project));
    }

    [HttpPost("create-project")]
    public async Task<IActionResult> CreatePosition([FromBody] ProjectCreateModel projectCreateModel, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        var result = await projectService.CreateProjectAsync(mapper.Map<ProjectModel>(projectCreateModel), adminId, cancellationToken);
        return Ok(mapper.Map<ProjectViewModel>(result));
    }
    
    [HttpDelete("{projectId:int}")]
    public async Task<IActionResult> DeletePosition(int projectId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await projectService.DeleteProjectAsync(projectId,managerId,cancellationToken);
        return Ok();
    }
}