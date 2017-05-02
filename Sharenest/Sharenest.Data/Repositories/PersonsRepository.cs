using Sharenest.Data.Interfaces;
using Sharenest.Models.EntityModels;

namespace Sharenest.Data.Repositories
{
    public class PersonsRepository : GenericRepository<Person>, IPersonsRepository
    {
        public PersonsRepository(IDbContext context) : base(context)
        {
        }
    }
}
