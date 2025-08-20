using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Helpers;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Enums;
using Office.DAL.Repositories.Intefaces;

namespace Office.BLL.Services;

public class LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
    IEmployeeRepository employeeRepository): ILeaveRequestService
{
    public async Task<LeaveRequestModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(id, cancellationToken);

        if (leaveRequest is null)
            throw new RequestException($"Leave request with Id {id} not found");

        var requestModel = mapper.Map<LeaveRequestModel>(leaveRequest);
        return requestModel;
    }

    public async Task<LeaveRequestModel> GetByRequestIdAsync(int employeeId, int requestId, CancellationToken cancellationToken = default)
    {
        var leaveRequestsDb = await leaveRequestRepository.GetAll().Include(r => r.Employee)
            .SingleOrDefaultAsync(r => r.Id == requestId, cancellationToken);
        
        return mapper.Map<LeaveRequestModel>(leaveRequestsDb);
    }

    public async Task<LeaveRequestModel> CreateLeaveRequestAsync(int employeeId, int approverId, LeaveRequestModel leaveRequestModel,
        CancellationToken cancellationToken)
    {
        var employeeDb = await employeeRepository.GetAll()
            .FirstOrDefaultAsync(r => r.Id == employeeId && r is Employee, cancellationToken);
        
        if (employeeDb is not Employee)
            throw new EmployeeNotFoundException($"Employee with Id {employeeId} not found");
        
        var requestDb = await leaveRequestRepository.CreateLeaveRequestAsync(new LeaveRequest
            {
                EmployeeId = employeeId,
                AbsenceReasonId = leaveRequestModel.AbsenceReasonId,
                ApprovalRequest = new ApprovalRequest
                {
                    ApproverId = approverId,
                    Comment = ""
                },
                StartDate = leaveRequestModel.StartDate,
                EndDate = leaveRequestModel.EndDate,
                Status = LeaveRequestStatus.Submit,
                Comment = leaveRequestModel.Comment
            },
            cancellationToken);
        
        var employeeDays  = requestDb.Employee.OutOfOfficeBalance;
        var daysOff = (requestDb.EndDate - requestDb.StartDate).Days;
        
        if(daysOff > employeeDays)
            throw new OutOfBalanceLimitException("Employee doesn't have enough days on balance");
        
        return mapper.Map<LeaveRequestModel>(requestDb);
    }

    public async Task<LeaveRequestModel> UpdateLeaveRequestAsync(
        int employeeId, 
        LeaveRequestModel leaveRequestModel,
        CancellationToken cancellationToken = default)
    {
        var employeeDb = await employeeRepository.GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == employeeId, cancellationToken);

        if (employeeDb is null)
            throw new NotPermissionException("User not found");
        
        var leaveRequestDb = await leaveRequestRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == leaveRequestModel.Id, cancellationToken);

        if (leaveRequestDb is null)
            throw new RequestException($"Leave request with Id {leaveRequestModel.Id} not found");
        
        if (employeeDb is Employee && leaveRequestDb.EmployeeId != employeeId)
            throw new NotPermissionException("You don't have permissions");
        
        foreach (var propertyMap in ReflectionHelper.WidgetUtil<LeaveRequestModel, LeaveRequest>.PropertyMap)
        {
            var sourceProp = propertyMap.Item1;
            var targetProp = propertyMap.Item2;

            var sourceValue = sourceProp.GetValue(leaveRequestModel);
            var targetValue = targetProp.GetValue(leaveRequestDb);

            if (sourceProp.Name != "EmployeeId" &&
                sourceValue != null &&
                !(sourceValue is string str && str == "") &&
                !Equals(sourceValue, default(DateTime)) &&
                !Equals(sourceValue, targetValue))
            {
                targetProp.SetValue(leaveRequestDb, sourceValue);
            }
        }
        
        await leaveRequestRepository.UpdateLeaveRequestAsync(leaveRequestDb, cancellationToken);
        return mapper.Map<LeaveRequestModel>(leaveRequestDb);
    }
    
    public async Task DeleteLeaveRequestAsync(int employeeId, int leaveRequestId, CancellationToken cancellationToken = default)
    {
        var employeeDb = await employeeRepository.GetAll()
            .FirstOrDefaultAsync(e => e.Id == employeeId && (e is Employee || e is Admin), cancellationToken);
        if (employeeDb is null)
            throw new NotPermissionException("You don't have permissions");
        
        var query = leaveRequestRepository.GetAll()
            .Include(r => r.Employee);
        
        LeaveRequest? leaveRequestDb = employeeDb is Employee
            ? await query.SingleOrDefaultAsync(r => r.EmployeeId == employeeId && r.Id == leaveRequestId, cancellationToken)
            : await query.SingleOrDefaultAsync(r => r.Id == leaveRequestId, cancellationToken);

        if (leaveRequestDb is null)
            throw new RequestException($"Leave request with Id {leaveRequestId} not found or access denied.");

        await leaveRequestRepository.DeleteLeaveRequestAsync(leaveRequestDb, cancellationToken);
    }
    
    public async Task<List<LeaveRequestModel>> GetAllAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        var userDb = await employeeRepository.GetAll()
            .FirstOrDefaultAsync(r => r.Id == employeeId, cancellationToken);
        if (userDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {employeeId} not found");
        
        //admin can check all requests
        //hr, pm can check only own employee's request(employee must be pin to manager)
        var leaveRequestsDb = userDb switch
        {
            HrManager => await leaveRequestRepository.GetAll()
                .Where(r => r.Employee.HrManagerId == employeeId)
                .Include(r => r.Employee)
                .ToListAsync(cancellationToken),
            
            ProjectManager => await leaveRequestRepository.GetAll()
                .Where(r => r.Employee.Projects.Any(i => i.ProjectManagerId == employeeId))
                .Include(r => r.Employee)
                .ToListAsync(cancellationToken),
            
            Employee => await leaveRequestRepository.GetAll()
                .Where(r => r.EmployeeId == employeeId)
                .Include(r => r.Employee)
                .ToListAsync(cancellationToken),
            
            Admin => await leaveRequestRepository.GetAll()
                .Include(r => r.Employee)
                .ToListAsync(cancellationToken),
            
            _ => throw new EmployeeNotFoundException($"Employee with Id {employeeId} not found")
        };
        
        return mapper.Map<List<LeaveRequestModel>>(leaveRequestsDb);
    }
}