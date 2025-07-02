using Office.BLL.Models;

namespace Office.BLL.Services.Interfaces;

public interface ILeaveRequestService: IBasicService<LeaveRequestModel>
{
    Task<LeaveRequestModel> GetByRequestIdAsync(int employeeId, int requestId, CancellationToken cancellationToken = default);
    
    Task<LeaveRequestModel> CreateLeaveRequestAsync(int employeeId, int approverId,  LeaveRequestModel leaveRequestModel,
        CancellationToken cancellationToken);

    Task<LeaveRequestModel> UpdateLeaveRequestAsync(int employeeId, LeaveRequestModel leaveRequestModel,
        CancellationToken cancellationToken = default);

    Task DeleteLeaveRequestAsync(int employeeId, int leaveRequestId, CancellationToken cancellationToken = default);
    Task<List<LeaveRequestModel>> GetAllAsync(int employeeId, CancellationToken cancellationToken = default);
}