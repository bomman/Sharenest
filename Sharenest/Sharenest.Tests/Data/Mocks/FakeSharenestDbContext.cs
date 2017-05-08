using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sharenest.Data.Interfaces;
using Sharenest.Models.EntityModels;

namespace Sharenest.Tests.Data.Mocks
{
    public class FakeSharenestDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public FakeSharenestDbContext()
        {
            this.Homes = new FakeHomesDbSet();
        }

        public DbSet<Home> Homes { get; set; }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
