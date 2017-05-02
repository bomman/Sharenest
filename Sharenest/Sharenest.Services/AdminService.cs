using System.Collections.Generic;
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

        public AdminService(IHomesRepository homesRepository, IPersonsRepository personsRepository, IDbContext context) : base(context)
        {
            this.homesRepository = homesRepository;
            this.personsRepository = personsRepository;
        }

        public IEnumerable<AdminHomesViewModel> GetAllHomes()
        {
            IEnumerable<Home> homes = this.homesRepository.Get();
            var homesViewModels = AutoMapper.Mapper.Map<IEnumerable<Home>, IEnumerable<AdminHomesViewModel>>(homes);

            return homesViewModels;
        }
    }
}
