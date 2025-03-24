using Office.BLL.Models;

namespace Office.BLL.Services.Interfaces;

public interface IPositionService: IBasicService<PositionModel>
{
    Task<PositionModel> CreatePositionAsync(PositionModel position,int managerId, CancellationToken cancellationToken = default);
    Task DeletePositionAsync(int positionId, CancellationToken cancellationToken = default);
    Task UpdatePositionAsync(PositionModel position, CancellationToken cancellationToken = default);
}