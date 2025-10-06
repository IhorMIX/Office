using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class TaskRepository(OfficeDbContext officeDbContext) : ITaskRepository
{
    public IQueryable<TaskEntity> GetAll()
    {
        return officeDbContext.Tasks.Include(r => r.Employees)
            .Include(r => r.Project);
    }

    public async Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await officeDbContext.Tasks
            .Include(r => r.Employees)
            .Include(r => r.Project)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<TaskEntity> CreateTaskAsync(TaskEntity taskEntity, CancellationToken cancellationToken = default)
    {
        var entityEntry =  await officeDbContext.Tasks.AddAsync(taskEntity, cancellationToken);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    public async Task DeleteTaskAsync(TaskEntity taskEntity, CancellationToken cancellationToken = default)
    {
        officeDbContext.Tasks.Remove(taskEntity);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTaskAsync(TaskEntity taskEntity, CancellationToken cancellationToken = default)
    {
        officeDbContext.Tasks.Update(taskEntity);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }
}