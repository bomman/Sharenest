using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        public IEnumerable<AdminPersonsViewModel> GetAllPerons()
        {
            var persons = this.personsRepository.Get().ToArray();
            var personsViewModels =
                AutoMapper.Mapper.Map<IEnumerable<Person>, IEnumerable<AdminPersonsViewModel>>(persons).ToArray();

            var roles = personsRepository.GetRoles().ToList();
            for (int i = 0; i < personsViewModels.Length; i++)
            {
                var userRoles = persons[i].User.Roles.ToList();

                personsViewModels[i].Email = persons[i].User.Email;
                AdminRoleViewModel role = new AdminRoleViewModel();
                foreach (IdentityUserRole userRole in userRoles)
                {
                    foreach (IdentityRole identityRole in roles)
                    {
                        if (userRole.RoleId == identityRole.Id)
                        {
                            role.Name = identityRole.Name;
                            role.IsSelected = true;

                            break;
                        }
                    }
                }

                personsViewModels[i].Role = role;
            }

            return personsViewModels;
        }
    }
}
