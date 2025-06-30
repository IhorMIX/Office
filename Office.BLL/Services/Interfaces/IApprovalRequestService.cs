using Office.BLL.Models;
using Office.DAL.Entity;

namespace Office.BLL.Services.Interfaces;

public interface IApprovalRequestService : IBasicService<ApprovalRequestModel>
{
    Task<List<ApprovalRequestModel>> GetApprovalRequestsAsync(int userId, CancellationToken cancellationToken = default);
    Task<ApprovalRequestModel> ApproveLeaveRequestAsync(int managerId, int requestId, string comment,
        CancellationToken cancellationToken = default);
    
    Task<ApprovalRequestModel> DeclineLeaveRequestAsync(int managerId,  int requestId, string comment,
        CancellationToken cancellationToken = default);
}