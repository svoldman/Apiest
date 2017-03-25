using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Apiest.Features.Common.Core.Interfaces
{
    public interface IRepository<TDomainModel>
                    where TDomainModel : class, IEntity
    {
        IQueryable<TDomainModel> Where(Expression<Func<TDomainModel,bool>> expression);
        IQueryable<TDomainModel> AsQueryable();
        void Add(TDomainModel domainEntity);
        void Remove(TDomainModel domainEntity);
        void Update(TDomainModel domainEntity);
        void Save();
   }
}
