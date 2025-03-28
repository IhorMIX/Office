using Office.BLL.Models;
using Office.DAL.Entity.Selections;

namespace Office.BLL.Services.Interfaces;

public interface IAbsenceReasonService: IBasicService<AbsenceReasonModel>
{
    Task<AbsenceReason> CreatePositionAsync(string absenseDesc,int managerId, CancellationToken cancellationToken = default);
    Task DeletePositionAsync(int positionId, CancellationToken cancellationToken = default);
    Task UpdatePositionAsync(AbsenceReasonModel position, CancellationToken cancellationToken = default);
}