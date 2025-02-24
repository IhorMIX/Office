using Office.DAL.Entity.Selections;

namespace Office.DAL.Repositories.Intefaces;

public interface IAbsenceReasonRepository : IBasicRepository<AbsenceReason>
{
    Task<AbsenceReason> CreateAbsenceReason(AbsenceReason request, CancellationToken cancellationToken = default);
    Task DeleteAbsenceReasonAsync(AbsenceReason request, CancellationToken cancellationToken = default);
    
    Task UpdateAbsenceReasonAsync(AbsenceReason request, CancellationToken cancellationToken = default);
}