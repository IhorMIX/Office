using Office.BLL.Models;
using Office.DAL.Entity.Selections;

namespace Office.BLL.Services.Interfaces;

public interface IPositionService: IBasicService<PositionModel>
{
    Task<PositionModel> CreatePositionAsync(PositionModel position,int managerId, CancellationToken cancellationToken = default);
    Task DeletePositionAsync(int positionId, int managerId, CancellationToken cancellationToken = default);
    Task UpdatePositionAsync(int managerId, Position position,
        CancellationToken cancellationToken = default);
    Task<List<Position>> GetAllAsync(CancellationToken cancellationToken);
}