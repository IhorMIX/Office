using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class AbsenceReasonRepository(OfficeDbContext officeDbContext) : IAbsenceReasonRepository
{
    public IQueryable<AbsenceReason> GetAll()
    {
        return officeDbContext.AbsenceReasons.AsQueryable();
    }

    public async Task<AbsenceReason?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await officeDbContext.AbsenceReasons.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<AbsenceReason> CreateAbsenceReason(AbsenceReason request, CancellationToken cancellationToken = default)
    {
        var entityEntry = await officeDbContext.AbsenceReasons.AddAsync(request, cancellationToken);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteAbsenceReasonAsync(AbsenceReason request, CancellationToken cancellationToken = default)
    {
        officeDbContext.AbsenceReasons.Remove(request);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAbsenceReasonAsync(AbsenceReason request, CancellationToken cancellationToken = default)
    {
        officeDbContext.AbsenceReasons.Update(request);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }
}