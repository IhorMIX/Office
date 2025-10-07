using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Helpers;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Repositories.Intefaces;
using TaskStatus = Office.DAL.Entity.Enums.TaskStatus;

namespace Office.BLL.Services;

public class TaskService(ITaskRepository taskRepository, IMapper mapper,
    IEmployeeRepository employeeRepository, IProjectRepository projectRepository): ITaskService
{
    public async Task<TaskEntityModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var taskDb = await taskRepository.GetByIdAsync(id, cancellationToken);
        if (taskDb is null)
            throw new EntityNotFoundException($"Task with Id {id} not found");
        
        var task = mapper.Map<TaskEntityModel>(taskDb);
        return task;
    }

    public async Task<TaskEntityModel> CreateTaskAsync(
        int creatorId,
        TaskEntityModel taskEntityModel,
        CancellationToken cancellationToken)
    {
        var creatorDb = await employeeRepository.GetAll()
            .Where(r => r.Id == creatorId && (r is ProjectManager || r is Admin))
            .FirstOrDefaultAsync(cancellationToken);

        if (creatorDb is null)
            throw new NotPermissionException("You don't have permissions");

        var exists = await taskRepository.GetAll()
            .AnyAsync(t => t.Title == taskEntityModel.Title, cancellationToken);

        if (exists)
            throw new AlreadyDataException($"Task with title '{taskEntityModel.Title}' already exists");

        var taskEntity = new TaskEntity
        {
            ProjectId = taskEntityModel.ProjectId,
            StartDate = taskEntityModel.StartDate,
            EndDate = taskEntityModel.EndDate,
            TaskStatus = TaskStatus.New,
            Title = taskEntityModel.Title,
            Description = taskEntityModel.Description,
            Employees = new List<Employee>()
        };

        var taskDb = await taskRepository.CreateTaskAsync(taskEntity, cancellationToken);
        return mapper.Map<TaskEntityModel>(taskDb);
    }
    
    public async Task<TaskEntityModel> UpdateTaskAsync(
        int managerId,
        TaskEntityModel taskEntityModel,
        CancellationToken cancellationToken = default)
    {
        var managerDb = await employeeRepository.GetAll()
            .FirstOrDefaultAsync(r => r.Id == managerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (managerDb is null)
            throw new NotPermissionException("You don't have permissions");

        var taskDb = await taskRepository.GetByIdAsync(taskEntityModel.Id, cancellationToken);
        if (taskDb is null)
            throw new EntityNotFoundException($"Task with Id {taskEntityModel.Id} not found");
        
        var oldStatus = taskDb.TaskStatus;
        
        foreach (var propertyMap in ReflectionHelper.WidgetUtil<TaskEntityModel, TaskEntity>.PropertyMap)
        {
            var userProperty = propertyMap.Item1;
            var userDbProperty = propertyMap.Item2;

            if (userProperty.Name == nameof(TaskEntity.TaskStatus))
                continue;

            var userSourceValue = userProperty.GetValue(taskEntityModel);
            var userTargetValue = userDbProperty.GetValue(taskDb);

            if (userSourceValue != null && !ReferenceEquals(userSourceValue, "") &&
                !userSourceValue.Equals(userTargetValue))
            {
                userDbProperty.SetValue(taskDb, userSourceValue);
            }
        }

        taskDb.TaskStatus = oldStatus;
        
        await taskRepository.UpdateTaskAsync(taskDb, cancellationToken);
        return mapper.Map<TaskEntityModel>(taskDb);
    }

    public async Task AssignTaskAsync(
        int managerId,
        int taskId,
        ICollection<int> employeeIds,
        CancellationToken cancellationToken = default)
    {
        if (await employeeRepository.GetAllManagers()
                .AllAsync(r => r.Id != managerId || !(r is ProjectManager || r is Admin), cancellationToken))
            throw new NotPermissionException("You don't have permissions");
        
        var taskDb = await taskRepository.GetAll()
                         .Include(t => t.Employees)
                         .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken)
                     ?? throw new EntityNotFoundException($"Task with Id {taskId} not found");
        
        var employeesToAssign = await employeeRepository.GetAllEmployees()
            .Where(e => employeeIds.Contains(e.Id))
            .ToListAsync(cancellationToken);

        if (employeesToAssign.Count != employeeIds.Count)
        {
            var missing = employeeIds.Except(employeesToAssign.Select(e => e.Id));
            throw new EntityNotFoundException($"Employees with Ids [{string.Join(", ", missing)}] not found");
        }
        
        taskDb.Employees = employeesToAssign;
        taskDb.TaskStatus = TaskStatus.InProgress;

        await taskRepository.UpdateTaskAsync(taskDb, cancellationToken);
    }
    
    public async Task DeleteTaskAsync(int managerId, int taskId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is ProjectManager || r is Admin))
            .FirstOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new NotPermissionException("You don't have permissions");
        
        var taskDb = await taskRepository.GetByIdAsync(taskId, cancellationToken);
        
        if (taskDb is null)
            throw new EntityNotFoundException($"Task with Id {taskId} not found");
        await taskRepository.DeleteTaskAsync(taskDb, cancellationToken);
    }

    public async Task<List<TaskEntityModel>> GetAllAsync(
        int employeeId,
        CancellationToken cancellationToken = default)
    {
        var employeeDb = await employeeRepository.GetByIdAsync(employeeId, cancellationToken)
                         ?? throw new EntityNotFoundException($"Employee with Id {employeeId} not found");

        IQueryable<TaskEntity> query = taskRepository.GetAll();

        query = employeeDb switch
        {
            Admin => query,
            ProjectManager pm => query.Where(t => t.Project!.ProjectManagerId == pm.Id),
            Employee => query.Where(t => t.Employees.Any(e => e.Id == employeeId)),
            _ => throw new NotPermissionException("You don't have permissions")
        };

        var tasks = await query.ToListAsync(cancellationToken);
        return mapper.Map<List<TaskEntityModel>>(tasks);
    }
}