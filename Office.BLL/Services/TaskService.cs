using AutoMapper;
using Office.BLL.Exceptions;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Repositories.Intefaces;

namespace Office.BLL.Services;

public class TaskService(ITaskRepository taskRepository, IMapper mapper,
    IEmployeeRepository employeeRepository, IProjectRepository projectRepository): ITaskInterface
{
    public async Task<TaskEntityModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var taskDb = await taskRepository.GetByIdAsync(id, cancellationToken);
        if (taskDb is null)
            throw new EntityNotFoundException($"Task with Id {id} not found");
        
        var task = mapper.Map<TaskEntityModel>(taskDb);
        return task;
    }

    public Task<TaskEntityModel> CreateTaskAsync(int creatorId, TaskEntityModel taskEntityModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TaskEntityModel> UpdateTaskAsync(int managerId, LeaveRequestModel leaveRequestModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTaskAsync(int managerId, int taskId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TaskEntityModel>> GetAllAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}