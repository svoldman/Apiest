using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apiest.Features.Common.Core.Interfaces
{
    public interface IMother
    {
        List<IEnumerable<IEntity>> Data { get; }
    }
}
