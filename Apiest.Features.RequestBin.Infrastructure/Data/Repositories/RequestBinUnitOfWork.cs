using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;
using Apiest.Features.Common.Infrastructure.Data.Repositories;
using Apiest.Features.RequestBin.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Models;
using Apiest.Features.RequestBin.Infrastructure.Data.Mapping;
using WebRequest = Apiest.Features.RequestBin.Core.Models.WebRequest;
using WebResponse = Apiest.Features.RequestBin.Core.Models.WebResponse;

namespace Apiest.Features.RequestBin.Infrastructure.Data.Repositories
{
    public class RequestBinUnitOfWork : UnitOfWork, IRequestBinUnitOfWork
    {
        public RequestBinUnitOfWork(DbContext context) : base(context)
        {
            var mappingProfile = new RequestBinMapping();
        }

        public IRepository<WebRequestGroup> WebRequestGroup => new Repository<WebRequestGroup, Models.WebRequestGroup>(base.DbContext);
        public IRepository<WebRequest> WebRequest => new Repository<WebRequest, Models.WebRequest>(base.DbContext);
        public IRepository<WebResponse> WebResponce => new Repository<WebResponse, Models.WebResponse>(base.DbContext);
        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
