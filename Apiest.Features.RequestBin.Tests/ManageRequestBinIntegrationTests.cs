using System;
using Apiest.Features.RequestBin.Core.Interfaces;
using Apiest.Features.RequestBin.Infrastructure.Data;
using Apiest.Features.RequestBin.Infrastructure.Data.Repositories;
using Apiest.Features.RequestBin.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apiest.Features.RequestBin.Tests
{
  
    [TestClass]
    public class ManageRequestBinIntegrationTests
    {
        private string _connectionString;
        
        public  ManageRequestBinIntegrationTests()
        {
            _connectionString =
                   "workstation id=ApiHooks.mssql.somee.com;packet size=4096;user id=svoldman_SQLLogin_1;pwd=de5i636acy;data source=ApiHooks.mssql.somee.com;persist security info=False;initial catalog=ApiHooks";

        }

        [TestMethod]
        public void CreateNewRequestBinGroupVerifyIfRequestBinGroupHasBeenCreatedDeleteRequestBin()
        {
            using (var context = new RequestBinEfContext(_connectionString))
            using (var requestBinUnitOfWork = new RequestBinUnitOfWork(context))
            {
                IRequestBinService requestBinService = new RequestBinService(requestBinUnitOfWork);
                requestBinService.CreateGroup("TestGroupName");
                var group = requestBinService.GetGroup("TestGroupName");
                requestBinService.RemoveGroup(group);
                Assert.IsNotNull(group);
            }
        }
    }
}
