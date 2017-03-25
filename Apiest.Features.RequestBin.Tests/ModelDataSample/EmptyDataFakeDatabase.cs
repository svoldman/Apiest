using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Models;

namespace Apiest.Features.RequestBin.Tests.ModelDataSample
{
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
}
