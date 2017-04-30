using System.Collections.Generic;
using Sharenest.Data;
using Sharenest.Data.Interfaces;
using Sharenest.Models.BindingModels;
using Sharenest.Models.ViewModels.Homes;
using Sharenest.Services.Interfaces;

namespace Sharenest.Services
{
    public class HomesService : Service, IHomesService
    {
        private IHomesRepository repository;

        public HomesService(IHomesRepository repository, IDbContext context) : base(context)
        {
            this.repository = repository;
        }

        public ICollection<HomesIndexViewModel> GetLastSixPostedHomes()
        {
            //TODO:
            throw new System.NotImplementedException();
        }

        public HomeDetailsViewModel GetHomeDetailsViewModelById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void AddHome(AddHomeBindingModel home)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateHome(UpdateHomeBindingModel home)
        {
            throw new System.NotImplementedException();
        }

        public HomeEditViewModel GetHomeEditViewModelById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteHomeById(int id)
        {
            this.repository.Delete(id);
        }

        public void Dispose(bool disposing)
        {
            //TODO:
        }

        public HomeDetailsViewModel GetHomeDetailsViewModelById(int? id)
        {
            //TODO:
            throw new System.NotImplementedException();
        }
    }
}
