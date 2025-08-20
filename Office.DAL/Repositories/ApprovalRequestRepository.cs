using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class ApprovalRequestRepository(OfficeDbContext officeDbContext) : IApprovalRequestRepository
{
    public IQueryable<ApprovalRequest> GetAll()
    {
        return officeDbContext.ApprovalRequests.AsNoTracking();
    }

    public async Task<ApprovalRequest?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await officeDbContext.ApprovalRequests.Include(r => r.LeaveRequest)
            .ThenInclude(lr => lr.Employee).SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<ApprovalRequest> CreateApprovalRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default)
    {
        var entityEntry = await officeDbContext.ApprovalRequests.AddAsync(request, cancellationToken);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteApprovalRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default)
    {
        officeDbContext.ApprovalRequests.Remove(request);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateApprovalRequestAsync(ApprovalRequest request, CancellationToken cancellationToken = default)
    {
        officeDbContext.ApprovalRequests.Update(request);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }
}