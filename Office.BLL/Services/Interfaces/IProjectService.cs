using Office.BLL.Models;

namespace Office.BLL.Services.Interfaces;

public interface IProjectService: IBasicService<ProjectModel>
{
    Task<ProjectModel> CreateProjectAsync(ProjectModel projectModel, int managerId,
        CancellationToken cancellationToken = default);
    Task DeleteProjectAsync(int projectId, int managerId, CancellationToken cancellationToken = default);

    Task<ProjectModel> UpdateProjectAsync(int projectManagerId, ProjectModel projectModel,
        CancellationToken cancellationToken = default);

    Task AddEmployeesInProjectAsync(int projectManagerId, int projectId,
        ICollection<int> employeeModelsIds, CancellationToken cancellationToken = default);
}