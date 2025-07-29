using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.BLL.Services;

public class SubdivisionService(ISubdivisionRepository subdivisionRepository, IMapper mapper,IEmployeeRepository employeeRepository) : ISubdivisionService
{
    public async Task<SubdivisionModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var subdivisionDb = await subdivisionRepository.GetByIdAsync(id, cancellationToken);
        
        if (subdivisionDb is null)
            throw new EntityNotFoundException($"Subdivision with Id {id} not found");
        
        var subdivision = mapper.Map<SubdivisionModel>(subdivisionDb);
        return subdivision;
    }
    
    public async Task<SubdivisionModel> CreateSubdivisionAsync(SubdivisionModel subdivisionModel,int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is HrManager || r is Admin))
            .SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new NotPermissionException("You don't have permissions");
        
        var subdivisionDb = await subdivisionRepository.GetAll().FirstOrDefaultAsync(i => i.Name == subdivisionModel.Name, cancellationToken);
        if (subdivisionDb is not null)
        {
            if (subdivisionDb.Name == subdivisionModel.Name)
                throw new AlreadyDataException("Subdivision is already created");
        }
        
        var subdivision = await subdivisionRepository.CreateSubdivisionAsync(mapper.Map<Subdivision>(subdivisionModel), cancellationToken);
        return mapper.Map<SubdivisionModel>(await subdivisionRepository.GetByIdAsync(subdivision.Id, cancellationToken));
    }
    
    public async Task DeleteSubdivisionAsync(int subdivisionId, int managerId,CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is HrManager || r is Admin))
            .SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new NotPermissionException("You don't have permissions");
        
        var subdivisionDb = await subdivisionRepository.GetByIdAsync(subdivisionId, cancellationToken);
        
        if (subdivisionDb is null)
            throw new EntityNotFoundException($"Subdivision with Id {subdivisionId} not found");
        await subdivisionRepository.DeleteSubdivisionAsync(subdivisionDb, cancellationToken);
    }
    
    public async Task UpdateSubdivisionAsync(int managerId, Subdivision subdivision, CancellationToken cancellationToken = default)
    {
        var managerDb = await employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new NotPermissionException("You don't have permissions");

        var subdivisionCheck = await subdivisionRepository.GetAll().Where(r => r.Name == subdivision.Name)
            .SingleOrDefaultAsync(cancellationToken);
        if (subdivisionCheck != null)
            throw new AlreadyDataException($"Subdivision with name {subdivision.Name} created already");
        
        var subdivisionDb = await subdivisionRepository.GetByIdAsync(subdivision.Id, cancellationToken);
        if (subdivisionDb is null)
            throw new EntityNotFoundException($"Subdivision with id {subdivision.Id} not found");

        subdivisionDb.Name = subdivision.Name;

        await subdivisionRepository.UpdateSubdivisionAsync(subdivisionDb, cancellationToken);
    }
}