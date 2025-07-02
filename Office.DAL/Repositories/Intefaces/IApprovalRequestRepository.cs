using Office.DAL.Entity;

namespace Office.DAL.Repositories.Intefaces;

public interface IApprovalRequestRepository : IBasicRepository<ApprovalRequest>
{
    Task<ApprovalRequest> CreateApprovalRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
    Task DeleteApprovalRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
    Task UpdateApprovalRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default);
}