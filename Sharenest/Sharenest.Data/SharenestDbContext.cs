using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sharenest.Data.Interfaces;
using Sharenest.Models.EntityModels;

namespace Sharenest.Data
{
    public class SharenestDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public SharenestDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Home> Homes { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Person> Persons { get; set; }

        public static SharenestDbContext Create()
        {
            return new SharenestDbContext();
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}