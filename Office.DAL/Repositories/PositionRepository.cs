using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class PositionRepository(OfficeDbContext officeDbContext) : IPositionRepository
{
    public IQueryable<Position> GetAll()
    {
        return officeDbContext.Positions.AsQueryable();
    }

    public async Task<Position?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await officeDbContext.Positions.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Position> CreatePositionAsync(Position position, CancellationToken cancellationToken = default)
    {
        var entityEntry = await officeDbContext.Positions.AddAsync(position, cancellationToken);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeletePositionAsync(Position position, CancellationToken cancellationToken = default)
    {
        officeDbContext.Positions.Remove(position);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePositionAsync(Position position, CancellationToken cancellationToken = default)
    {
        officeDbContext.Positions.Update(position);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }
}