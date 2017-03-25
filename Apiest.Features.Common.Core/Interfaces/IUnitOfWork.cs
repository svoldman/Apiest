using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apiest.Features.Common.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }
    }
}
