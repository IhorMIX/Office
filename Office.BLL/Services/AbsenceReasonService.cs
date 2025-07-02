using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.BLL.Services;

public class AbsenceReasonService(IAbsenceReasonRepository absenceReasonRepository, IMapper mapper, IEmployeeRepository employeeRepository) : IAbsenceReasonService
{
    public async Task<AbsenceReasonModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var absenceReasonDb = await absenceReasonRepository.GetByIdAsync(id, cancellationToken);
        
        if (absenceReasonDb is null)
            throw new EntityNotFoundException($"Absence Reason with Id {id} not found");
        
        var absenceReason = mapper.Map<AbsenceReasonModel>(absenceReasonDb);
        return absenceReason;
    }

    public async Task<AbsenceReason> CreateAbsenceReasonAsync(string description, int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is HrManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Admin or Hr Manager with Id {managerId} not found");
        
        var absenceReasonDb = await absenceReasonRepository.GetAll().FirstOrDefaultAsync(i => i.ReasonDescription == description, cancellationToken);
        if (absenceReasonDb is not null)
        {
            if (absenceReasonDb.ReasonDescription == description)
                throw new AlreadyDataException("Absence Reason is already created");
        }
        
        var absenceReason  = await absenceReasonRepository.CreateAbsenceReason(new AbsenceReason()
        {
            ReasonDescription = description,
        }, cancellationToken);

        return absenceReason;
    }

    public async Task DeleteAbsenceReasonAsync(int absenceReasonId, int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is HrManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Admin or Hr Manager with Id {managerId} not found");
        
        var absenceReasonDb = await absenceReasonRepository.GetByIdAsync(absenceReasonId, cancellationToken);
        
        if (absenceReasonDb is null)
            throw new EntityNotFoundException($"Absence Reason with Id {absenceReasonId} not found");
        await absenceReasonRepository.DeleteAbsenceReasonAsync(absenceReasonDb, cancellationToken);
    }

    public Task UpdateAbsenceReasonAsync(AbsenceReasonModel absenceReasonModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}