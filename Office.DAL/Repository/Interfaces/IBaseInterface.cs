namespace Office.DAL.Repository.Interfaces;

public interface IBaseInterface<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}