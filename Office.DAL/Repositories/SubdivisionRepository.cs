using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class SubdivisionRepository(OfficeDbContext officeDbContext) : ISubdivisionRepository
{
    public IQueryable<Subdivision> GetAll()
    {
        return officeDbContext.Subdivisions.AsNoTracking();
    }

    public async Task<Subdivision?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await officeDbContext.Subdivisions.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Subdivision> CreateSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default)
    {
        var entity = await officeDbContext.Subdivisions.AddAsync(subdivision,cancellationToken);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    public async Task DeleteSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default)
    {
        officeDbContext.Subdivisions.Remove(subdivision);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default)
    {
        officeDbContext.Subdivisions.Update(subdivision);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }
}