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
    public async Task<IActionResult> CreateAbsenceReason(AbsenceReasonCreateModel absenceReasonCreateModel, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        var result = await absenceReasonService.CreateAbsenceReasonAsync(absenceReasonCreateModel.Name, adminId, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{absenceReasonId:int}")]
    public async Task<IActionResult> DeleteAbsenceReason(int absenceReasonId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await absenceReasonService.DeleteAbsenceReasonAsync(absenceReasonId,managerId, cancellationToken);
        return Ok();
    }
} 