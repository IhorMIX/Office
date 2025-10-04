using Office.DAL.Entity;
using Office.DAL.Entity.Employees;

namespace Office.DAL.Repositories.Intefaces;

public interface ITaskRepository : IBasicRepository<TaskEntity>
{
    Task<TaskEntity> CreateTaskAsync(TaskEntity taskEntity, CancellationToken cancellationToken = default);
    Task DeleteTaskAsync(TaskEntity taskEntity, CancellationToken cancellationToken = default);
    Task UpdateTaskAsync(TaskEntity taskEntity, CancellationToken cancellationToken = default);
}