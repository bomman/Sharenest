using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Sharenest.Data.Interfaces;
using Sharenest.Models.EntityModels;

namespace Sharenest.Data.Repositories
{
    public class PersonsRepository : GenericRepository<Person>, IPersonsRepository
    {
        public PersonsRepository(IDbContext context) : base(context)
        {
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return context.Roles.ToList();
        }
    }
}
