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
public class LeaveRequestController(IMapper mapper, ILeaveRequestService leaveRequestService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllLeaveRequest(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var request = await leaveRequestService.GetAllAsync(userId, cancellationToken);
        return Ok(mapper.Map<List<LeaveRequestFullViewModel>>(request));
    }
    
    [HttpGet("{requestId:int}")]
    public async Task<IActionResult> GetLeaveRequest(int requestId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var request = await leaveRequestService.GetByRequestIdAsync(userId, requestId, cancellationToken);
        return Ok(mapper.Map<LeaveRequestFullViewModel>(request));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestCreateModel requestModel, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var request = await leaveRequestService.CreateLeaveRequestAsync(userId, requestModel.ApproverId, mapper.Map<LeaveRequestModel>(requestModel) , cancellationToken);
        return Ok(mapper.Map<LeaveRequestViewModel>(request));
    }
    
    [HttpDelete("{requestId:int}")]
    public async Task<IActionResult> DeleteLeaveRequest(int requestId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await leaveRequestService.DeleteLeaveRequestAsync(userId, requestId, cancellationToken);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateLeaveRequest([FromBody] LeaveRequestUpdateModel requestModel, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await leaveRequestService.UpdateLeaveRequestAsync(userId, mapper.Map<LeaveRequestModel>(requestModel), cancellationToken);
        return Ok();
    }
}