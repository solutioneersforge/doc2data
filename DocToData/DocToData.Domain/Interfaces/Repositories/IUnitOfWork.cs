using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>(string contextType) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
