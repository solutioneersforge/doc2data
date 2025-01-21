using DocToData.Domain.Enum;
using Microsoft.EntityFrameworkCore;


namespace DocToData.Domain.Factories
{
    public interface IDBContextFactory
    {
        DbContext GetDbContext(DBContextEnum dBContextEnum);
    }
}
