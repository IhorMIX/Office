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
            throw new EntityNotFoundException($"Position with Id {id} not found");
        
        var position = mapper.Map<PositionModel>(positionDb);
        return position;
    }

    public async Task<PositionModel> CreatePositionAsync(PositionModel positionModel,int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetByIdAsync(managerId, cancellationToken);
        if (creator is not (HrManager or Admin))
            throw new NotPermissionException("You don't have permissions");
        
        var positionDb = await positionRepository.GetAll().FirstOrDefaultAsync(i => i.Name == positionModel.Name, cancellationToken);
        if (positionDb is not null)
        {
            if (positionDb.Name == positionModel.Name)
                throw new AlreadyDataException("Position is already created");
        }
        
        positionDb = await positionRepository.CreatePositionAsync(mapper.Map<Position>(positionModel), cancellationToken);
        return mapper.Map<PositionModel>(await positionRepository.GetByIdAsync(positionDb.Id, cancellationToken));
    }

    public async Task DeletePositionAsync(int positionId,int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetByIdAsync(managerId, cancellationToken);
        if (creator is not (HrManager or Admin))
            throw new NotPermissionException("You don't have permissions");
        
        var positionDb = await positionRepository.GetByIdAsync(positionId, cancellationToken);
        
        if (positionDb is null)
            throw new EntityNotFoundException($"Position with Id {positionId} not found");
        await positionRepository.DeletePositionAsync(positionDb, cancellationToken);
    }

    public async Task UpdatePositionAsync(int managerId, Position position, CancellationToken cancellationToken = default)
    {
        var managerDb = await employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new NotPermissionException("You don't have permissions");

        var positionCheck = await positionRepository.GetAll().Where(r => r.Name == position.Name)
            .SingleOrDefaultAsync(cancellationToken);
        if (positionCheck != null)
            throw new AlreadyDataException($"Position with name {position.Name} created already");
        
        var positionDb = await positionRepository.GetByIdAsync(position.Id, cancellationToken);
        if (positionDb is null)
            throw new EntityNotFoundException($"Position with id {position.Id} not found");

        positionDb.Name = position.Name;

        await positionRepository.UpdatePositionAsync(positionDb, cancellationToken);
    }
}