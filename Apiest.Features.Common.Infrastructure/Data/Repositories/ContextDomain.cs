using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;

namespace Apiest.Features.Common.Infrastructure.Data.Repositories
{
    public class ContextDomain
    {
        public ContextDomain(List<IEnumerable<IEntity>> models )
        {
            Context = new Dictionary<string, IEnumerable<IEntity>>();
            foreach (var model in models)
            {
                if (model.GetType().GetProperties().Length >= 2)
                {
                    Context.Add(model.GetType().GetProperties()[2].PropertyType.Name, model);
                }
               
            }
        }

        public Dictionary<string,IEnumerable<IEntity>> Context { get; set; }
    }
}
