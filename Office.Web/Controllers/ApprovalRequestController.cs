using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office.BLL.Services.Interfaces;
using Office.Web.Extensions;
using Office.Web.Models;

namespace Office.Web.Controllers;

[Authorize]

[Route("api/[controller]")]
[ApiController]
public class ApprovalRequestController(IMapper mapper, IApprovalRequestService approvalRequestService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetApprovalRequest(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var approvedRequest = await approvalRequestService.GetApprovalRequestsAsync(userId, cancellationToken);
        return Ok(mapper.Map<List<ApprovalRequestViewModel>>(approvedRequest));
    }
    
    [HttpPut("approve")]
    public async Task<IActionResult> ApproveRequest([FromBody] ApprovalRequestUpdateModel approve, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var approvedRequest = await approvalRequestService.ApproveLeaveRequestAsync(userId, approve.Id, approve.Comment,
            cancellationToken);
        return Ok(mapper.Map<ApprovalRequestViewModel>(approvedRequest));
    }
    
    [HttpPut("decline")]
    public async Task<IActionResult> DeclineRequest([FromBody] ApprovalRequestUpdateModel approve, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var approvedRequest = await approvalRequestService.DeclineLeaveRequestAsync(userId, approve.Id, approve.Comment,
            cancellationToken);
        return Ok(mapper.Map<ApprovalRequestViewModel>(approvedRequest));
    }
    
    
}