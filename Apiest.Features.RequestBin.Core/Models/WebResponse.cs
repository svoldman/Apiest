using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;

namespace Apiest.Features.RequestBin.Core.Models
{
    [Serializable]

    public class WebResponse : IEntity
    {
        public int Id { get; set; }

        public string Body { get; set; }
    }
}
