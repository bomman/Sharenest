using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Sharenest.Models.EntityModels;

namespace Sharenest.Data.Interfaces
{
    public interface IPersonsRepository : IGenericRepository<Person>
    {
        IEnumerable<IdentityRole> GetRoles();
    }
}
