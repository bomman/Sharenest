using System.Collections.Generic;
using Sharenest.Models.BindingModels;
using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Homes;

namespace Sharenest.Services.Interfaces
{
    public interface IHomesService
    {
        ICollection<HomesIndexViewModel> GetLastSixPostedHomes();
        HomeDetailsViewModel GetHomeDetailsViewModelById(int id);
        void AddHome(AddHomeBindingModel home);
        void UpdateHome(UpdateHomeBindingModel home);
        HomeEditViewModel GetHomeEditViewModelById(int id);
        void DeleteHomeById(int id);
        void Dispose(bool disposing);
    }
}
