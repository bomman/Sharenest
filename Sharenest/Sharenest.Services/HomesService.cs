using System;
using System.Collections.Generic;
using System.Linq;
using Dropbox.Api;
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

        public HomesService(IHomesRepository repository) : base()
        {
            this.repository = repository;
        }

        public IEnumerable<HomesIndexViewModel> GetLastSixPostedHomes()
        {
            var homes = repository.Get()
                .OrderBy(home => home.PostedDate)
                .Take(6);

            foreach (var home in homes)
            {
                home.ProfilePicture = ConvertPath(home.ProfilePicture);
            }

            var viewModels = AutoMapper.Mapper.Map<IEnumerable<Home>, IEnumerable<HomesIndexViewModel>>(homes);
            return viewModels;
        }

        public HomeDetailsViewModel GetHomeDetailsViewModelById(int id)
        {
            var model = repository.GetByID(id);
            var home = 
                AutoMapper.Mapper.Map<Home, HomeDetailsViewModel>(model);

            if (home?.ProfilePicture != null)
            {
                home.ProfilePicture = ConvertPath(home.ProfilePicture);
            }

            //home.Location.LocationName = model.Location.LocationName;

            return home;
        }

        public void AddHome(AddHomeBindingModel home)
        {
            var homeToAdd = AutoMapper.Mapper.Map<AddHomeBindingModel, Home>(home);

            if (home.ProfilePicture != null)
            {
                var profilePicture = PictureHelper.ConvertToBytes(home.ProfilePicture);

                string linkToProfilePicture = String.Empty;
                using (var dbx = new DropboxClient(DropboxHelper.AccessToken))
                {
                    linkToProfilePicture = DropboxHelper.Upload(dbx, "/Homes/" + home.Name, "profile.png", profilePicture).ToString();
                }

                homeToAdd.ProfilePicture = linkToProfilePicture;
            }
            else
            {
                homeToAdd.ProfilePicture = "/Defaults/home.png";
            }

            GeocodingHelper.SetLocation(homeToAdd.Location);
            homeToAdd.PostedDate = DateTime.Now;
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
           // editViewModel.Location.LocationName = home.Location.LocationName;

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

        private string ConvertPath(string path)
        {
            int lastSlash = path.LastIndexOf('/');

            string folder = path.Substring(0, lastSlash);
            string file = path.Substring(lastSlash);

            using (var dbx = new DropboxClient(DropboxHelper.AccessToken))
            {
                byte[] imageByteData = DropboxHelper.Download(dbx, folder, file);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

                return imageDataURL;
            }   
        }
    }
}
