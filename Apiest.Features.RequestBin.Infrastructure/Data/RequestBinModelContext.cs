using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;
using Apiest.Features.RequestBin.Core.Models;

namespace Apiest.Features.RequestBin.Infrastructure.Data
{
    public  class RequestBinModelContext : IDisposable
    {
        public RequestBinModelContext(List<IEnumerable<IEntity>> dataModels )
        {
            foreach (var dataModel in dataModels)
            {
                if (dataModel is List<WebRequest>)
                {
                    WebRequest = new List<WebRequest>();
                    WebRequest.AddRange((List<WebRequest>)dataModel);
                }
                if (dataModel is List<WebRequestGroup>)
                {
                    WebRequestGroup = new List<WebRequestGroup>();
                    WebRequestGroup.AddRange((List<WebRequestGroup>)dataModel);
                }
                if (dataModel is List<WebResponse>)
                {
                    WebResponse = new List<WebResponse>();
                    WebResponse.AddRange((List<WebResponse>)dataModel);
                }
            }


            
           
        }
        public   List<WebRequest> WebRequest { get; set; }
        public  List<WebRequestGroup> WebRequestGroup { get; set; }
        public List<WebResponse> WebResponse { get; set; }
        public void Dispose()
        {
            WebRequestGroup = null;
            WebRequest = null;
            WebResponse = null;
        }

        // some comments
        public void SaveChanges()
        {
            
        }
    }
}
