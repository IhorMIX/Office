using Office.DAL.Entity.Selections;

namespace Office.DAL.Repositories.Intefaces;

public interface IProjectTypeRepository : IBasicRepository<ProjectType>
{
    Task<ProjectType> CreateProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default);
    Task DeleteProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default);
    Task UpdateProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default);
}