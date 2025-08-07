using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Enums;
using Office.DAL.Repositories.Intefaces;

namespace Office.BLL.Services;

public class ApprovalRequestService(IApprovalRequestRepository approvalRequestRepository,
    IEmployeeRepository employeeRepository, IMapper mapper, ILeaveRequestRepository leaveRequestRepository): IApprovalRequestService
{
    public async Task<ApprovalRequestModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var requestDb = await approvalRequestRepository.GetByIdAsync(id, cancellationToken);

        if (requestDb is null)
            throw new RequestException($"Approval request with Id {id} not found");

        var approvalRequestModel = mapper.Map<ApprovalRequestModel>(requestDb);
        return approvalRequestModel;
    }

    public async Task<List<ApprovalRequestModel>> GetApprovalRequestsAsync(int userId, CancellationToken cancellationToken = default)
    {
        var userDb = await employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == userId, cancellationToken);
        if (userDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {userId} not found");
        
        var requests = userDb switch
        {
            HrManager => await approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Where(r => r.ApproverId == userId)
                .ToListAsync(cancellationToken),

            ProjectManager => await approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Where(r => r.ApproverId == userId)
                .ToListAsync(cancellationToken),

            Employee => await approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .Include(r => r.LeaveRequest)
                .Where(r => r.LeaveRequest.EmployeeId == userId)
                .ToListAsync(cancellationToken),

            Admin => await approvalRequestRepository.GetAll()
                .Include(r => r.Approver)
                .Include(r=> r.LeaveRequest)
                .ToListAsync(cancellationToken),

            _ => throw new EmployeeNotFoundException($"Employee with Id {userId} not found")
        };
        return mapper.Map<List<ApprovalRequestModel>>(requests);
    }

    public async Task<ApprovalRequestModel> ApproveLeaveRequestAsync(int managerId, int requestId, string comment,
        CancellationToken cancellationToken = default)
    {
        var managerDb = await employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is Employee), cancellationToken);
        if (managerDb is null)
            throw new NotPermissionException("You don't have permissions");
        
        var requestDb = await approvalRequestRepository.GetByIdAsync(requestId, cancellationToken);
        if (requestDb is null)
            throw new RequestException($"Approval request with Id {requestId} not found");

        if (managerDb is Admin)
            requestDb.ApproverId = managerDb.Id;
        
        if(requestDb.ApproverId != managerId)
            throw new ManagerException($"Manger with Id {requestId} is not approver");
            
        var employee = requestDb.LeaveRequest.Employee;
        var daysOff = (requestDb.LeaveRequest.EndDate - requestDb.LeaveRequest.StartDate).Days;
        
        if (employee.OutOfOfficeBalance < daysOff)
            throw new OutOfBalanceLimitException("Employee doesn't have enough days on balance");

        employee.OutOfOfficeBalance -= daysOff;
        
        requestDb.Status = ApprovalRequestStatus.Approved;
        requestDb.Comment = comment;
        
        await employeeRepository.UpdateEmployeeAsync(employee, cancellationToken);
        await approvalRequestRepository.UpdateApprovalRequestAsync(requestDb, cancellationToken);
        
        return mapper.Map<ApprovalRequestModel>(requestDb);
    }

    public async Task<ApprovalRequestModel> RejectLeaveRequestAsync(int managerId, int requestId, string comment,
        CancellationToken cancellationToken = default)
    {
        var managerDb = await employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is Employee), cancellationToken);
        if (managerDb is null)
            throw new NotPermissionException("You don't have permissions");
        
        var requestDb = await approvalRequestRepository.GetByIdAsync(requestId, cancellationToken);
        if (requestDb is null)
            throw new RequestException($"Approval request with Id {requestId} not found");

        if (managerDb is Admin)
            requestDb.ApproverId = managerDb.Id;
        
        if(requestDb.ApproverId != managerId)
            throw new ManagerException($"manger with Id {requestId} is not approver");
        
        requestDb.Status = ApprovalRequestStatus.Rejected;
        requestDb.Comment = comment;
        await approvalRequestRepository.UpdateApprovalRequestAsync(requestDb, cancellationToken);
        return mapper.Map<ApprovalRequestModel>(requestDb);
    }
}