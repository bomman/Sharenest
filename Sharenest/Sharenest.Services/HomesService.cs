using System.Collections.Generic;
using System.Linq;
using Sharenest.Data.Interfaces;
using Sharenest.Models.BindingModels;
using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Homes;
using Sharenest.Services.Helpers;
using Sharenest.Services.Interfaces;

namespace Sharenest.Services
{
    public class HomesService : Service, IHomesService
    {
        private readonly IHomesRepository repository;

        public HomesService(IHomesRepository repository, IDbContext context) : base(context)
        {
            this.repository = repository;
        }

        public IEnumerable<HomesIndexViewModel> GetLastSixPostedHomes()
        {
            var homes = repository.Get()
                .OrderBy(home => home.PostedDate)
                .Take(6);

            var viewModels = AutoMapper.Mapper.Map<IEnumerable<Home>, IEnumerable<HomesIndexViewModel>>(homes);
            return viewModels;
        }

        public HomeDetailsViewModel GetHomeDetailsViewModelById(int id)
        {
            var home = 
                AutoMapper.Mapper.Map<Home, HomeDetailsViewModel>(
                repository.GetByID(id)
                );

            return home;
        }

        public void AddHome(AddHomeBindingModel home)
        {
            var homeToAdd = AutoMapper.Mapper.Map<AddHomeBindingModel, Home>(home);
            var profilePicture = PictureHelper.ConvertToBytes(home.ProfilePicture);

            repository.Insert(homeToAdd);
            this.repository.Commit();
        }

        public void UpdateHome(UpdateHomeBindingModel home)
        {
            var homeToUpdate = AutoMapper.Mapper.Map<UpdateHomeBindingModel, Home>(home);
            repository.Update(homeToUpdate);
            this.repository.Commit();
        }

        public HomeEditViewModel GetHomeEditViewModelById(int id)
        {
            var home = repository.GetByID(id);
            var editViewModel = AutoMapper.Mapper.Map<Home, HomeEditViewModel>(home);

            return editViewModel;
        }

        public void DeleteHomeById(int id)
        {
            this.repository.Delete(id);
            this.repository.Commit();
        }

        public HomeEditViewModel ChangeUpdateHomeBindingModelToHomesEditViewModel(UpdateHomeBindingModel home)
        {
            return AutoMapper.Mapper.Map<UpdateHomeBindingModel, HomeEditViewModel>(home);
        }

        public void UpdateHomeRating(DatailsRateBindingModel model)
        {
            Home home = this.repository.GetByID(model.Id);
            home.Rating = (home.Rating + model.Rating) / 2;

            this.repository.Update(home);
            this.repository.Commit();
        }
    }
}
