using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apiest.Features.Common.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Interfaces;
using Apiest.Features.RequestBin.Infrastructure.Data;
using Apiest.Features.RequestBin.Infrastructure.Data.Repositories;

namespace webservice.App_Start
{

    public class InfrastructureDataModelContext : IInfrastructureDataContext
    {
        private readonly IMother _motherData;

        public InfrastructureDataModelContext()
        {

        }

        public InfrastructureDataModelContext(IMother motherData)
        {
            _motherData = motherData;
        }

        public void InitializeRequestBinUnitOfWork(Action<IRequestBinUnitOfWork> action)
        {
            using (var requestBinUnitOfWork = new RequestBinUnitOfWorkModel(_motherData))
            {
                action.Invoke(requestBinUnitOfWork);
            }
        }
    }

    public class InfrastructureDataContext : IInfrastructureDataContext
    {
        private readonly string _connectionString;

        public InfrastructureDataContext()
        {
            _connectionString =
                System.Configuration.ConfigurationManager.ConnectionStrings["ApiHooks"].ConnectionString;

        }

        public void InitializeRequestBinUnitOfWork(Action<IRequestBinUnitOfWork> action)
        {
            using (var context = new RequestBinEfContext(_connectionString))
            using (var requestBinUnitOfWork = new RequestBinUnitOfWork(context))
            {
                action.Invoke(requestBinUnitOfWork);
            }
        }
    }


}