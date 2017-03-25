using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Models;

namespace Apiest.Features.RequestBin.Core.Interfaces
{
    public interface IRequestBinUnitOfWork
    {
        IRepository<WebRequestGroup> WebRequestGroup { get; }
        IRepository<WebRequest> WebRequest { get; }
        IRepository<WebResponse> WebResponce { get; }
        void Commit();
    }
}
