using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Sharenest.Data.Interfaces;
using Sharenest.Models.EntityModels;

namespace Sharenest.Tests.Data
{
    public interface ISharenestDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        DbSet<Home> Homes { get; set; }
    }
}
