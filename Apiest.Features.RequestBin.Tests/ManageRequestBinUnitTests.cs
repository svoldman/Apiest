using System;
using System.Linq;
using Apiest.Features.RequestBin.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Models;
using Apiest.Features.RequestBin.Infrastructure.Data.Repositories;
using Apiest.Features.RequestBin.Services.Services;
using Apiest.Features.RequestBin.Tests.ModelDataSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apiest.Features.RequestBin.Tests
{
    [TestClass]
    public class ManageRequestBinUnitTests
    {
        [TestMethod]
        public void CreateNewRequestBinGroupVerifyIfRequestBinGroupHasBeenCreated()
        {
            using (var requestBinUnitOfWork = new RequestBinUnitOfWorkModel(new EmptyDataFakeDatabase()))
            {
                IRequestBinService requestBinService = new RequestBinService(requestBinUnitOfWork);
                requestBinService.CreateGroup("TestGroupName");
                var group = requestBinService.GetGroup("TestGroupName");

                Assert.IsNotNull(group);
            }
        
        }
    }

  

    
}
