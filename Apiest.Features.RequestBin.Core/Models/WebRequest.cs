using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;

namespace Apiest.Features.RequestBin.Core.Models
{
    [Serializable]
    public class WebRequest : IEntity
    {
        public int Id { get; set; }
        public int WebRequestGroupId { get; set; }
        public string Body { get; set; }
        public string Header { get; set; }
        public string QueryString { get; set; }
    }
}
