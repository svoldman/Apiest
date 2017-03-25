using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;

namespace Apiest.Features.Common.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

        public DbContext DbContext => _context;
    }
}
