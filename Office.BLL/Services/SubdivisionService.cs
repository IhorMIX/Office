using Office.BLL.Models;
using Office.BLL.Services.Interfaces;

namespace Office.BLL.Services;

public class SubdivisionService : ISubdivisionService
{
    public Task<SubdivisionModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}