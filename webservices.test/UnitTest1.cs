using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Apiest.Features.Common.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using webservice;
using webservice.App_Start;
using webservice.Controllers;

namespace webservices.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoadRequestBinWithEmptyDataIndexView()
        {
            // arange
            var controller = new RequestBinController(new InfrastructureDataModelContext(new EmptyDataFakeDatabase()));

            // act
            var action = controller.Index(string.Empty) as ActionResult;

            // assert
            Assert.IsNotNull(action);
        }

        [TestMethod]
        public void LoadRequestBinGroupsWithNotEmptyDataIndexView()
        {
            // arange
            IEnumerable<WebRequestGroup>  modelResult = null;
            var controller = new RequestBinController(new InfrastructureDataModelContext(new NotEmptyDataFakeDatabase()));

            // act
            ViewResult viewResult = controller.Index(string.Empty) as ViewResult;

            if (viewResult.Model is IEnumerable<WebRequestGroup>)
            {
                modelResult = ((IEnumerable<WebRequestGroup>) viewResult.Model).ToList();
            }

            // assert
            Assert.IsNotNull(modelResult);

        }

    }

    public class EmptyDataFakeDatabase : IMother
    {
        public List<IEnumerable<IEntity>> Data
        {
            get
            {
                return new List<IEnumerable<IEntity>>
                {
                    new List<WebRequest>(),
                    new List<WebRequestGroup>(),
                    new List<WebResponse>()

                };
            }
        }
    }

    public class NotEmptyDataFakeDatabase : IMother
    {
        public List<IEnumerable<IEntity>> Data
        {
            get
            {
                return new List<IEnumerable<IEntity>>
                {
                    new List<WebRequest> {new WebRequest { Id = 1, Body = "body test", Header = "header test",QueryString = "query string test",WebRequestGroupId = 1} },
                    new List<WebRequestGroup> {new WebRequestGroup {Id = 1,GroupUniqueId = Guid.NewGuid(),Name = "Test Group"} },
                    new List<WebResponse>()

                };
            }
        }
    }
}
