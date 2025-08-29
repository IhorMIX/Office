using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class LeaveRequestRepository(OfficeDbContext officeDbContext) : ILeaveRequestRepository
{
    public IQueryable<LeaveRequest> GetAll()
    {
        return officeDbContext.LeaveRequests.Include(r => r.AbsenceReason)
            .Include(r => r.ApprovalRequest).ThenInclude(r=>r.Approver)
            .AsNoTracking();
    }

    public async Task<LeaveRequest?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await officeDbContext.LeaveRequests.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default)
    {
        var entityEntry =  await officeDbContext.LeaveRequests.AddAsync(request, cancellationToken);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default)
    {
        officeDbContext.LeaveRequests.Remove(request);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateLeaveRequestAsync(LeaveRequest request, CancellationToken cancellationToken = default)
    {
        officeDbContext.LeaveRequests.Update(request);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }
}