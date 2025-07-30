using Office.BLL.Models;
using Office.DAL.Entity.Selections;

namespace Office.BLL.Services.Interfaces;

public interface ISubdivisionService: IBasicService<SubdivisionModel>
{
    Task<SubdivisionModel> CreateSubdivisionAsync(SubdivisionModel subdivisionModel, int managerId,
        CancellationToken cancellationToken = default);
    Task DeleteSubdivisionAsync(int subdivisionId,int managerId, CancellationToken cancellationToken = default);
    Task UpdateSubdivisionAsync(int managerId, Subdivision subdivision,
        CancellationToken cancellationToken = default);
    Task<List<Subdivision>> GetAllAsync(CancellationToken cancellationToken);
}