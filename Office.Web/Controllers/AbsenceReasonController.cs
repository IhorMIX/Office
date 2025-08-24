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
public class AbsenceReasonController(IAbsenceReasonService absenceReasonService, IMapper mapper)
    : ControllerBase
{
    [HttpGet("{absenceReasonId:int}")]
    public async Task<IActionResult> GetById(int absenceReasonId, CancellationToken cancellationToken = default)
    {
        var absenceReason = await absenceReasonService.GetByIdAsync(absenceReasonId, cancellationToken);
        return Ok(mapper.Map<AbsenceReasonViewModel>(absenceReason));
    }

    [HttpPost("create-absenceReason")]
    public async Task<IActionResult> CreateAbsenceReason([FromBody] ReasonRequest reasonRequest, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        var result = await absenceReasonService.CreateAbsenceReasonAsync(reasonRequest.ReasonDescription, adminId, cancellationToken);
        return Ok(mapper.Map<AbsenceReasonViewModel>(result));
    }
    
    [HttpDelete("{absenceReasonId:int}")]
    public async Task<IActionResult> DeleteAbsenceReason(int absenceReasonId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await absenceReasonService.DeleteAbsenceReasonAsync(absenceReasonId,managerId, cancellationToken);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAbsenceReason([FromBody] AbsenceReasonViewModel absenceReason,
        CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await absenceReasonService.UpdateAbsenceReasonAsync(userId, mapper.Map<AbsenceReason>(absenceReason), cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAbsenceReasons(CancellationToken cancellationToken = default)
    {
        var absenceReasons = await absenceReasonService.GetAllAsync(cancellationToken);
        return Ok(mapper.Map<List<AbsenceReasonViewModel>>(absenceReasons));
    }
} 