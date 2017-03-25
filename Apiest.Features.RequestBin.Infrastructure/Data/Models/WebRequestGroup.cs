using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Apiest.Features.RequestBin.Infrastructure.Data.Models
{
    public class WebRequestGroup
    {
        public WebRequestGroup()
        {}

        public int WebRequestGroupId { get; set; }
        public string Name { get; set; }

        public Guid GroupUniqueId { get; set; }

        public ICollection<WebRequest> WebRequests { get; set; }
        
    }
}
