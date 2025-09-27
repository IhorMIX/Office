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
public class TaskController(ITaskService taskService, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{taskId:int}")]
    public async Task<IActionResult> GetById(int taskId, CancellationToken cancellationToken = default)
    {
        var project = await taskService.GetByIdAsync(taskId, cancellationToken);
        return Ok(mapper.Map<TaskViewModel>(project));
    }
    
    [HttpPost("create-task")]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateModel taskCreateModel, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        var result = await taskService.CreateTaskAsync(adminId, mapper.Map<TaskEntityModel>(taskCreateModel),  cancellationToken);
        return Ok(mapper.Map<TaskViewModel>(result));
    }
    
    [HttpDelete("{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int taskId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await taskService.DeleteTaskAsync(managerId,taskId,cancellationToken);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody]TaskUpdateModel taskUpdateModel, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        var project= await taskService.UpdateTaskAsync(managerId,mapper.Map<TaskEntityModel>(taskUpdateModel), cancellationToken);
        return Ok(mapper.Map<TaskViewModel>(project));
    }
}