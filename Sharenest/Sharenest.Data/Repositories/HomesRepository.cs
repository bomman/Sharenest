using Sharenest.Data.Interfaces;
using Sharenest.Models.EntityModels;

namespace Sharenest.Data.Repositories
{
    public class HomesRepository : GenericRepository<Home>, IHomesRepository
    {
        public HomesRepository(IDbContext context) : base(context)
        {
        }
    }
}
