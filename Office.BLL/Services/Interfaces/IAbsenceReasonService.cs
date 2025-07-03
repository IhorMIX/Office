using Office.BLL.Models;
using Office.DAL.Entity.Selections;

namespace Office.BLL.Services.Interfaces;

public interface IAbsenceReasonService: IBasicService<AbsenceReasonModel>
{
    Task<AbsenceReason> CreateAbsenceReasonAsync(string description,int managerId, CancellationToken cancellationToken = default);
    Task DeleteAbsenceReasonAsync(int positionId,int managerId, CancellationToken cancellationToken = default);
    Task UpdateAbsenceReasonAsync(int managerId, AbsenceReason absenceReason,
        CancellationToken cancellationToken = default);
}