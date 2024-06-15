using Office.DAL.Entity;
using Office.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Office.DAL.Repository
{
    public class BaseUserRepository : IBaseUserRepository
    {
        private readonly OfficeDbContext _officeDbContext;

        public BaseUserRepository(OfficeDbContext officeDbContext)
        {
            _officeDbContext = officeDbContext;
        }

        public async Task<BaseUser?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _officeDbContext.Users
                .SingleOrDefaultAsync(i => i.Id == id, cancellationToken);
        }
    }
}