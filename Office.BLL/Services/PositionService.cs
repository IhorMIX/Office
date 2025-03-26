using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Helpers;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.BLL.Services;

public class PositionService(IPositionRepository positionRepository, IMapper mapper, IEmployeeRepository employeeRepository) : IPositionService
{
    public async Task<PositionModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var positionDb = await positionRepository.GetByIdAsync(id, cancellationToken);
        
        if (positionDb is null)
            throw new PositionNotFoundException($"Position with Id {id} not found");
        
        var position = mapper.Map<PositionModel>(positionDb);
        return position;
    }

    public async Task<PositionModel> CreatePositionAsync(PositionModel positionModel,int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is HrManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Manager with Id {managerId} not found");
        
        var positionDb = await positionRepository.GetAll().FirstOrDefaultAsync(i => i.Name == positionModel.Name, cancellationToken);
        if (positionDb is not null)
        {
            if (positionDb.Name == positionModel.Name)
                throw new AlreadyPositionException("Position is already created");
        }
        
        positionDb = await positionRepository.CreatePositionAsync(mapper.Map<Position>(positionModel), cancellationToken);
        return mapper.Map<PositionModel>(await positionRepository.GetByIdAsync(positionDb.Id, cancellationToken));
    }

    public async Task DeletePositionAsync(int positionId, CancellationToken cancellationToken = default)
    {
        var positionDb = await positionRepository.GetByIdAsync(positionId, cancellationToken);
        
        if (positionDb is null)
            throw new PositionNotFoundException($"Position with Id {positionId} not found");
        await positionRepository.DeletePositionAsync(positionDb, cancellationToken);
    }

    public Task UpdatePositionAsync(PositionModel position, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}