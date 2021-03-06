﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using Sharenest.Models.EntityModels;

namespace Sharenest.Data.Interfaces
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

        DbSet<Home> Homes { get; set; }

        IDbSet<IdentityRole> Roles { get; set; }
    }
}
