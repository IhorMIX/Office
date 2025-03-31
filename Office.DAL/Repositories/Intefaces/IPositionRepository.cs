using Office.DAL.Entity.Selections;

namespace Office.DAL.Repositories.Intefaces;

public interface IPositionRepository : IBasicRepository<Position>
{
    Task<Position> CreatePositionAsync(Position position, CancellationToken cancellationToken = default);
    Task DeletePositionAsync(Position position, CancellationToken cancellationToken = default);
    
    Task UpdatePositionAsync(Position position, CancellationToken cancellationToken = default);
}