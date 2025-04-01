using Office.BLL.Models;

namespace Office.BLL.Services.Interfaces;

public interface IProjectTypeService: IBasicService<ProjectTypeModel>
{
    Task<ProjectTypeModel> CreateProjectTypeAsync(ProjectTypeModel projectTypeModel, int managerId,
        CancellationToken cancellationToken = default);
    Task DeleteProjectTypeAsync(int projectTypeId,int managerId, CancellationToken cancellationToken = default);
}