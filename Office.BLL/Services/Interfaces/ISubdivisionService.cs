using Office.BLL.Models;

namespace Office.BLL.Services.Interfaces;

public interface ISubdivisionService: IBasicService<SubdivisionModel>
{
    Task<SubdivisionModel> CreateSubdivisionAsync(SubdivisionModel subdivisionModel, int managerId,
        CancellationToken cancellationToken = default);
    Task DeleteSubdivisionAsync(int subdivisionId,int managerId, CancellationToken cancellationToken = default);
}