using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sharenest.Models.EntityModels;

namespace Sharenest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sharenest.Data.SharenestDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Sharenest.Data.SharenestDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var userStore = new UserStore<ApplicationUser>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Roles.Any(role => role.Name == "Person"))
            {
                var role = new IdentityRole("Person");
                roleManager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var role = new IdentityRole("Admin");
                roleManager.Create(role);
            }

            if (!context.Users.Any())
            {
                var user = new ApplicationUser { UserName = "bomman", Email = "bggadbg@abv.bg"};
                userManager.Create(user, "1qazXSW@");
                userManager.AddToRole(user.Id, "Admin");
            }


            context.Homes.AddOrUpdate(new Home()
            {
                Name = "Dany's home",
                Activities = "going to the park, visiting the local university and playing voleyball",
                EndDate = DateTime.Today.AddDays(10),
                StartDate = DateTime.Today.AddDays(3),
                PostedDate = DateTime.Now,
                Location = new Location()
                {
                    Name = "Sliven",
                    Country = "Bulgaria",
                    Latitude = 42.6858333m,
                    Longitude = 26.3291667m
                },
                Provision = "bed for 2 plus parking place",
                Notes = "near the stadium Hadji Dimitar",
                ProfilePicture = "http://www.nasamnatam.com/usergal/411/Sl-4.JPG"
            });

            context.Homes.AddOrUpdate(new Home()
            {
                Name = "Gosho ot selo",
                Activities = "chistene na prasetata rano sutrin, duiene na kravite wecher i kopane na nivata prez denq",
                EndDate = DateTime.Today.AddDays(20),
                StartDate = DateTime.Today.AddDays(6),
                PostedDate = DateTime.Now,
                Location = new Location()
                {
                    Name = "Mechkarevo",
                    Country = "Bulgaria",
                    Latitude = 42.583333m,
                    Longitude = 26.283333m
                },
                Provision = "hamak ili palatka v dvora do kucheto",
                Notes = "shte usetite selskoto gostopriemstvo i ot chasti bita na horata",
                ProfilePicture = "https://img12.olx.bg/images_prodavalnikcom/101480360_4_171x132_pchelen-med-dom-i-gradina.jpg"
            });

            context.SaveChanges();

        }
    }
}
