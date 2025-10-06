using Office.DAL.Entity;
using Office.DAL.Entity.Employees;

namespace Office.DAL.Repositories.Intefaces;

public interface IProjectRepository : IBasicRepository<Project>
{
    Task<Project> CreateProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task DeleteProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);
}