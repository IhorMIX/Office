using Office.BLL.Models;
using Office.DAL.Entity;

namespace Office.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<BaseEmployee> GetByLoginAndPasswordAsync(string login, string password, CancellationToken cancellationToken = default);

    Task<BaseEmployeeModel> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    
    Task AddAuthorizationValueAsync(BaseEmployeeModel employeeModel, string refreshToken, DateTime? expiredDate = null, CancellationToken cancellationToken = default);

    Task LogOutAsync(int employeeId, CancellationToken cancellationToken = default);

    Task<BaseEmployeeModel> GetUserById(int userId, CancellationToken cancellationToken = default);
}