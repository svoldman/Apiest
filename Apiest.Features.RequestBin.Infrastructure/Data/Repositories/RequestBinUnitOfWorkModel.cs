using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;
using Apiest.Features.Common.Infrastructure.Data.Repositories;
using Apiest.Features.RequestBin.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Models;
using Apiest.Features.RequestBin.Infrastructure.Data;

namespace Apiest.Features.RequestBin.Infrastructure.Data.Repositories
{
    public class RequestBinUnitOfWorkModel : IRequestBinUnitOfWork, IDisposable
    {
        private readonly RequestBinModelContext _context;
        public RequestBinUnitOfWorkModel(IMother motherData)
        {
            _context = new RequestBinModelContext(motherData.Data);
        }

        public IRepository<WebRequestGroup> WebRequestGroup
        {
            get { return new RepositoryDomain<WebRequestGroup>(_context.WebRequestGroup); }
        }

        public IRepository<WebRequest> WebRequest
        {
            get { return new RepositoryDomain<WebRequest>(_context.WebRequest);}
        }

        public IRepository<WebResponse> WebResponce
        {
            get { return new RepositoryDomain<WebResponse>(_context.WebResponse);}
        }
        public RequestBinModelContext RequestBinModelContext { get { return _context; } }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
