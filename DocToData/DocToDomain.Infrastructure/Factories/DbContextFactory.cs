using DocToData.Domain.Factories;
using DocToDomain.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToDomain.Infrastructure.Factories
{
    public class DbContextFactory : IDBContextFactory
    {
        private readonly DocToDataDBContext _docToDataDBContext;

        public DbContextFactory(DocToDataDBContext docToDataDBContext)
        {
            this._docToDataDBContext = docToDataDBContext;
        }
        public DbContext GetDbContext(string contentType)
        {
            if(contentType.ToLower() == "DocToData".ToLower())
            {
                return _docToDataDBContext;
            }
            throw new ArgumentException("Invalid context type");
        }
    }
}
