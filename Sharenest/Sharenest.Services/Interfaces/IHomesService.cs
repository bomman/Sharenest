using System.Collections.Generic;
using Sharenest.Models.BindingModels;
using Sharenest.Models.ViewModels.Homes;

namespace Sharenest.Services.Interfaces
{
    public interface IHomesService
    {
        IEnumerable<HomesIndexViewModel> GetLastSixPostedHomes();
        HomeDetailsViewModel GetHomeDetailsViewModelById(int id);
        void AddHome(AddHomeBindingModel home);
        void UpdateHome(UpdateHomeBindingModel home);
        HomeEditViewModel GetHomeEditViewModelById(int id);
        void DeleteHomeById(int id);
        HomeEditViewModel ChangeUpdateHomeBindingModelToHomesEditViewModel(UpdateHomeBindingModel home);
    }
}
