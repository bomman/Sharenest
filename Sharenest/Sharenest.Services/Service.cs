using Sharenest.Data.Interfaces;

namespace Sharenest.Services
{
    public abstract class Service 
    {
        protected Service(IDbContext context)
        {
            this.Context = context;
        }

        public IDbContext Context { get; set; }
    }
}
