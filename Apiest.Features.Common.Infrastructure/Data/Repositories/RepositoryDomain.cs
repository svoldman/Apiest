using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;

namespace Apiest.Features.Common.Infrastructure.Data.Repositories
{
    public class RepositoryDomain<TDomainModel> : IRepository<TDomainModel>
                                        where TDomainModel : class, IEntity
    {
        public RepositoryDomain(List<TDomainModel> context )
        {
            DomainContext = context;
        }
        public IQueryable<TDomainModel> Where(Expression<Func<TDomainModel, bool>> expression)
        {
            return AsQueryable().Where(expression);
        }

        public IQueryable<TDomainModel> AsQueryable()
        {
            return DomainContext?.AsQueryable();
        }

        public void Add(TDomainModel domainEntity)
        {
            DomainContext.Add(domainEntity);
        }

        public void Remove(TDomainModel domainEntity)
        {
            var item = DomainContext.FirstOrDefault(p => p.Id == domainEntity.Id);
            if (item != null)
                DomainContext.Remove(item);
        }

        public void Update(TDomainModel domainEntity)
        {
            var item = DomainContext.FirstOrDefault(p => p.Id == domainEntity.Id);
            if (item != null)
            {
                DomainContext.Remove(item);
                DomainContext.Add(domainEntity);
            }
                
        }

        public List<TDomainModel> DomainContext { get; }
        public void Save()
        {
            
        }

        public void Update1(TDomainModel domainEntity)
        {
            throw new NotImplementedException();
        }
    }
}
