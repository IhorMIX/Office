using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office.BLL.Services.Interfaces;
using Office.Web.Models;

namespace Office.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TaskController(ITaskService taskService, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{taskId:int}")]
    public async Task<IActionResult> GetById(int taskId, CancellationToken cancellationToken = default)
    {
        var project = await taskService.GetByIdAsync(taskId, cancellationToken);
        return Ok(mapper.Map<TaskViewModel>(project));
    }
}