using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.RequestBin.Core.Interfaces;

namespace Apiest.Features.RequestBin.Core.Interfaces
{
    public interface IInfrastructureDataContext
    {
        void InitializeRequestBinUnitOfWork(Action<IRequestBinUnitOfWork> action);
    }
}
