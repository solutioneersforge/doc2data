using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToDomain.Infrastructure.Data
{
    public class DocToDataDBContext : DbContext
    {
        public DocToDataDBContext(DbContextOptions<DocToDataDBContext> dbContextOptions) : base(dbContextOptions) 
        {
            
        }
    }
}
