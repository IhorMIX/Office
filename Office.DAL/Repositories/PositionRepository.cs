using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class PositionRepository(OfficeDbContext officeDbContext) : IPositionRepository
{
    private readonly OfficeDbContext _officeDbContext = officeDbContext;

    public IQueryable<Position> GetAll()
    {
        return _officeDbContext.Positions.AsQueryable();
    }

    public async Task<Position?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.Positions.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Position> CreatePositionAsync(Position position, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _officeDbContext.Positions.AddAsync(position, cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeletePositionAsync(Position position, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Positions.Remove(position);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePositionAsync(Position position, CancellationToken cancellationToken = default)
    {
        _officeDbContext.Positions.Update(position);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }
}