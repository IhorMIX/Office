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
    private readonly IPositionRepository _positionRepository = positionRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    public async Task<PositionModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var positionDb = await positionRepository.GetByIdAsync(id, cancellationToken);
        
        if (positionDb is null)
            throw new PositionNotFoundException($"Employee with Id {id} not found");
        
        var position = mapper.Map<PositionModel>(positionDb);
        return position;
    }

    public async Task<PositionModel> CreatePositionAsync(PositionModel positionModel,int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await _employeeRepository.GetAll().Where(r => r.Id == managerId && (r is HrManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Manager with Id {managerId} not found");
        
        var positionDb = await _positionRepository.GetAll().FirstOrDefaultAsync(i => i.Name == positionModel.Name, cancellationToken);
        if (positionDb is not null)
        {
            if (positionDb.Name == positionModel.Name)
                throw new AlreadyPositionException("Position is already used by another employee");
        }
        
        positionDb = await _positionRepository.CreatePositionAsync(_mapper.Map<Position>(positionModel), cancellationToken);
        return _mapper.Map<PositionModel>(await _positionRepository.GetByIdAsync(positionDb.Id, cancellationToken));
    }

    public Task DeletePositionAsync(PositionModel position, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePositionAsync(PositionModel position, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}