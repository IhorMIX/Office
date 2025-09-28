using Office.BLL.Models;

namespace Office.BLL.Services.Interfaces;

public interface ITaskService: IBasicService<TaskEntityModel>
{
    Task<TaskEntityModel> CreateTaskAsync(int creatorId,  TaskEntityModel taskEntityModel,
        CancellationToken cancellationToken);

    Task<TaskEntityModel> UpdateTaskAsync(int managerId, TaskEntityModel taskEntityModel,
        CancellationToken cancellationToken = default);
    Task<TaskEntityModel> AssignTaskAsync(int managerId, TaskEntityModel taskEntityModel,
        CancellationToken cancellationToken = default);
    Task DeleteTaskAsync(int managerId, int taskId, CancellationToken cancellationToken = default);
    Task<List<TaskEntityModel>> GetAllAsync(int employeeId, CancellationToken cancellationToken = default);
}