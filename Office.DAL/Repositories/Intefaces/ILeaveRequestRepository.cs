using Office.DAL.Entity;

namespace Office.DAL.Repositories.Intefaces;

public interface ILeaveRequestRepository : IBasicRepository<LeaveRequest>
{
    Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default);
    Task DeleteLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default);
    Task UpdateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default);
}