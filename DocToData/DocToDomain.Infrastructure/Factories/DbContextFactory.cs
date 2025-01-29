using DocToData.Domain.Enum;
using DocToData.Domain.Factories;
using DocToDomain.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DocToDomain.Infrastructure.Factories;
public class DbContextFactory : IDBContextFactory
{
    private readonly DocToDataDBContext _docToDataDBContext;

    public DbContextFactory(DocToDataDBContext docToDataDBContext)
    {
        this._docToDataDBContext = docToDataDBContext;
    }
    public DbContext GetDbContext(DBContextEnum dBContextEnum)
    {
        if (dBContextEnum == DBContextEnum.DocToData)
        {
            return _docToDataDBContext;
        }
        throw new ArgumentException("Invalid context type");
    }
}

