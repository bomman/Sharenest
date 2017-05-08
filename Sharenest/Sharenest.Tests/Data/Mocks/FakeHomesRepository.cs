using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Sharenest.Data.Interfaces;
using Sharenest.Models.EntityModels;

namespace Sharenest.Tests.Data.Mocks
{
    public class FakeHomesRepository : FakeRepository<Home>, IHomesRepository
    {
        public FakeHomesRepository(IDbContext context) : base(context)
        {
        }

        public override IEnumerable<Home> Get(Expression<Func<Home, bool>> filter = null, Func<IQueryable<Home>, IOrderedQueryable<Home>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Home> query = this.context.Homes;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public override Home GetByID(object id)
        {
            return this.context.Homes.Find(id);
        }

        public override void Delete(object id)
        {
            var home = context.Homes.Find(id);
            if (home!= null)
            {
                context.Homes.Remove(home);
            }
        }

        public override void Insert(Home entity)
        {
            this.context.Homes.Add(entity);
        }
    }
}
