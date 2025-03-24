using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity.Selections;
using Office.Web.Extensions;
using Office.Web.Models;

namespace Office.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PositionController(IManagerService managerService, IEmployeeService employeeService,IPositionService positionService, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{position:int}")]
    public async Task<IActionResult> GetById(int positionId, CancellationToken cancellationToken = default)
    {
        var position = await positionService.GetByIdAsync(positionId, cancellationToken);
        return Ok(mapper.Map<PositionViewModel>(position));
    }

    [HttpPost("create-position")]
    public async Task<IActionResult> CreatePosition(PositionCreateModel positionCreateModel, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        var result = await positionService.CreatePositionAsync(mapper.Map<PositionModel>(positionCreateModel), adminId, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{positionId:int}")]
    public async Task<IActionResult> DeletePosition(int positionId, CancellationToken cancellationToken = default)
    {
        await positionService.DeletePositionAsync(positionId, cancellationToken);
        return Ok();
    }
} 