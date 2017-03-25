using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.Common.Core.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Apiest.Features.Common.Infrastructure.Data.Repositories
{
    public class Repository<TDomainModel, TCrmEntity> : IRepository<TDomainModel>
                        where TDomainModel : class, IEntity where TCrmEntity : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }
       
        public IQueryable<TDomainModel> Where(Expression<Func<TDomainModel, bool>> expression)
        {
            return this.AsQueryable()
                        .Where(expression)
                        .AsQueryable();
        }

        public IQueryable<TDomainModel> AsQueryable()
        {
            return this._context.Set<TCrmEntity>()
                                .ProjectTo<TDomainModel>()
                                .AsQueryable();
        }

        public void Add(TDomainModel domainEntity)
        {
            this._context.Set<TCrmEntity>()
                    .Add(Mapper.Map<TDomainModel,TCrmEntity>(domainEntity));
        }

        public void Remove(TDomainModel domainEntity)
        {
            var efEntity = Mapper.Map<TDomainModel, TCrmEntity>(domainEntity);
            var local = this._context.Set<TCrmEntity>()
                .Local
                .FirstOrDefault();
            if (local != null)
            {
                this._context.Entry(local).State = EntityState.Detached;
            }
            this._context.Entry(efEntity).State = EntityState.Deleted;
        }
    
        public void Update(TDomainModel domainEntity)
        {
            this._context.Database.Log = s => Debug.Write(s);
            var efObject = Mapper.Map<TDomainModel, TCrmEntity>(domainEntity);
            var entry = this._context.Entry(efObject);
            entry.State = EntityState.Modified;

            IEnumerable<PropertyMap> propertiesSourceMap = null;

            // get Domain mapping
            var typeMapSource = (Mapper.Configuration.GetAllTypeMaps()).FirstOrDefault(p => p.SourceType.Name.Equals(domainEntity.GetType().Name, StringComparison.CurrentCultureIgnoreCase));
            if (typeMapSource == null)
                return;

            // get all Source/Destination property mapping corresponding with Domain mapping
            propertiesSourceMap = typeMapSource.GetPropertyMaps();

            // get destination property names collection corresponding with a Domain source
            var propertySourceMapToDestination = (from prop in propertiesSourceMap
                                                  select new
                                                  {
                                                      prop.DestinationProperty.Name
                                                  }).ToList();

            if (propertySourceMapToDestination.Count == 0)
                return;


            // for each EF destination entity property modify only fields that have mapping with Domain
            foreach (var proName in entry.OriginalValues.PropertyNames)
            {
                try
                {
                    if (!propertySourceMapToDestination.Any(p => p.Name.Equals(proName, StringComparison.CurrentCultureIgnoreCase)))
                        entry.Property(proName).IsModified = false;
                }
                catch { }
            }
        }

        public void Save()
        {
            this._context.SaveChanges();
        }
        
    }
}
