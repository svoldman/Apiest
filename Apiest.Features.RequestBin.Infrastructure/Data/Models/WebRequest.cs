using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apiest.Features.RequestBin.Infrastructure.Data.Models
{
    public class WebRequest
    {
        public int WebRequestId { get; set; }
        public string Body { get; set; }
        public string Header { get; set; }
        public string QueryString { get; set; }

        public int WebRequestGroupId { get; set; }
        public WebRequestGroup WebRequestGroup { get; set; }
    }
}
