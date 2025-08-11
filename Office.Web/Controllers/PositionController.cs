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
public class PositionController(IPositionService positionService, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{positionId:int}")]
    public async Task<IActionResult> GetById(int positionId, CancellationToken cancellationToken = default)
    {
        var position = await positionService.GetByIdAsync(positionId, cancellationToken);
        return Ok(mapper.Map<SelectionViewModel>(position));
    }

    [HttpPost("create-position")]
    public async Task<IActionResult> CreatePosition(SelectionCreateModel positionCreateModel, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        var result = await positionService.CreatePositionAsync(mapper.Map<Position>(positionCreateModel), adminId, cancellationToken);
        return Ok(mapper.Map<SelectionViewModel>(result));
    }
    
    [HttpDelete("{positionId:int}")]
    public async Task<IActionResult> DeletePosition(int positionId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await positionService.DeletePositionAsync(positionId,managerId, cancellationToken);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SelectionViewModel position,
        CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await positionService.UpdatePositionAsync(userId, mapper.Map<Position>(position), cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPositions(CancellationToken cancellationToken = default)
    {
        var positions = await positionService.GetAllAsync(cancellationToken);
        return Ok(mapper.Map<List<SelectionViewModel>>(positions));
    }

} 