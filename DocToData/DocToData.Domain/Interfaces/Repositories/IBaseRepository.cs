using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Domain.Interfaces.Repositories;

public interface IBaseRepository<in T> where T : class
{
    Task<int> CreateEntity(T t);
    Task<int> UpdateEntity(Guid id, T t);
    Task<int> DeleteEntity(Guid id);
}

