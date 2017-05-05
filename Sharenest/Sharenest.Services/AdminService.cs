using System.Collections.Generic;
using System.Linq;
using Sharenest.Data.Interfaces;
using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Admin;
using Sharenest.Services.Interfaces;

namespace Sharenest.Services
{
    public class AdminService : Service, IAdminService
    {
        private readonly IHomesRepository homesRepository;
        private readonly IPersonsRepository personsRepository;

        public AdminService(IHomesRepository homesRepository, IPersonsRepository personsRepository) : base()
        {
            this.homesRepository = homesRepository;
            this.personsRepository = personsRepository;
        }

        public IEnumerable<AdminHomesViewModel> GetAllHomes()
        {
            IList<Home> homes = this.homesRepository.Get().ToList();
            var homesViewModels = AutoMapper.Mapper.Map<IEnumerable<Home>, IEnumerable<AdminHomesViewModel>>(homes).ToList();

            for (int i = 0; i < homes.Count; i++)
            {
                homesViewModels[i].Country = homes[i].Location.Country;
                homesViewModels[i].LocationName = homes[i].Location.LocationName;
            }
            return homesViewModels;
        }
    }
}
