using Office.DAL.Entity.Selections;

namespace Office.DAL.Repositories.Intefaces;

public interface ISubdivisionRepository : IBasicRepository<Subdivision>
{
    Task<Subdivision> CreateSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default);
    Task DeleteSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default);
    
    Task UpdateSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default);
}