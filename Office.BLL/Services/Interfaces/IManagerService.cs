using Office.BLL.Models;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;

namespace Office.BLL.Services.Interfaces;

public interface IManagerService
{
    Task<BaseEmployee> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<BaseManagerModel> CreateManagerAsync(int adminId, BaseManagerModel managerModel, CancellationToken cancellationToken = default);
    Task<BaseManagerModel> UpdateManagerAsync(int managerId, BaseManagerModel managerModel, CancellationToken cancellationToken = default);
    Task DeleteManagerAsync(int managerId, int adminId, CancellationToken cancellationToken = default);
    Task<List<BaseManagerModel>> GetAll(int adminId, CancellationToken cancellationToken = default);
    Task<List<HrManagerModel>> GetHrManagers(int adminId, CancellationToken cancellationToken = default);
    Task<List<ProjectManagerModel>> GetProjectManagers(int adminId, CancellationToken cancellationToken = default);
    Task<BaseManager> GetAdminAsync(CancellationToken cancellation = default);
    Task<List<BaseManager>> GetApproversAsync(int userId, CancellationToken cancellationToken = default);
}