using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Account;

namespace Sharenest.Services.Interfaces
{
    public interface IPersonsService
    {
        void AddPerson(RegisterViewModel model, ApplicationUser user);
    }
}
