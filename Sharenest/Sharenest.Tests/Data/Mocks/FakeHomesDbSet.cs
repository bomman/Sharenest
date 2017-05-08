using System.Linq;
using Sharenest.Data.Mocks;
using Sharenest.Models.EntityModels;

namespace Sharenest.Tests.Data.Mocks
{
    public class FakeHomesDbSet : FakeDbSet<Home>
    {
        public override Home Find(params object[] keyValues)
        {
            int wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(car => car.Id == wantedId);
        }
    }
}
