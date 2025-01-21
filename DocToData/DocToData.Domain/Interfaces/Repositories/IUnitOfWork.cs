using DocToData.Domain.Enum;

namespace DocToData.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>(DBContextEnum dBContextEnum) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
