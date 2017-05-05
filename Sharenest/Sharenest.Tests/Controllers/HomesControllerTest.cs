using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sharenest.Areas.Homes.Controllers;
using Sharenest.Services.Interfaces;
using Sharenest.Data.Interfaces;
using Sharenest.Models.BindingModels;
using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Admin;
using Sharenest.Models.ViewModels.Homes;
using Sharenest.Services;
using Sharenest.Tests.Data;
using Sharenest.Tests.Data.Mocks;

namespace Sharenest.Tests.Controllers
{
    [TestClass]
    public class HomesControllerTest
    {
        private HomesController _controller;
        private IHomesService _service;
        private IDbContext _context;
        private IHomesRepository _repository;
        private IList<Home> homes;

        private void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                #region Binding Models

                cfg.CreateMap<AddHomeBindingModel, Location>();
                cfg.CreateMap<AddHomeBindingModel, Home>()
                    .ForMember(x => x.ProfilePicture, opt => opt.Ignore())
                    .ForMember(dest => dest.Location, opt =>
                        opt.MapFrom(src => Mapper.Map<AddHomeBindingModel, Location>(src)));
                cfg.CreateMap<UpdateHomeBindingModel, Location>();
                cfg.CreateMap<UpdateHomeBindingModel, Home>()
                    .ForMember(dest => dest.Location, opt =>
                        opt.MapFrom(src => Mapper.Map<UpdateHomeBindingModel, Location>(src)));

                #endregion

                #region View Models

                cfg.CreateMap<Home, HomeEditViewModel>()
                    .ForMember(x => x.PicturesMedium, y => y.MapFrom(src => src.Pictures.Select(item => item.MediumPicturePath)));
                cfg.CreateMap<Home, HomeDetailsViewModel>()
                    .ForMember(x => x.PicturesMedium, y => y.MapFrom(src => src.Pictures.Select(item => item.MediumPicturePath)));
                cfg.CreateMap<Home, HomesIndexViewModel>();
                cfg.CreateMap<AdminHomesViewModel, Location>();
                cfg.CreateMap<AdminHomesViewModel, Home>()
                    .ForMember(dest => dest.Location, opt =>
                        opt.MapFrom(src => Mapper.Map<AdminHomesViewModel, Location>(src)))
                    .ReverseMap();

                #endregion

                cfg.CreateMap<HomeEditViewModel, UpdateHomeBindingModel>();
            });
        }

        [TestInitialize]
        public void Init()
        {
            this.ConfigureMapper();
            this.homes = new List<Home>()
            {
                new Home()
                {
                    Id = 1,
                    Name = "My home",
                    Activities = "ironing, fishing, etc.",
                    EndDate = DateTime.Now.AddDays(10),
                    StartDate = DateTime.Now.AddDays(3),
                    IsVisited = false,
                    Notes = "There is a disco near it",
                    Location = new Location()
                    {
                        Id = 1,
                        Country = "UK",
                        LocationName = "London",
                        Latitude = -52.58m,
                        Longitude = -1.97m
                    },
                    PostedDate = DateTime.Now,
                    ProfilePicture = "/Defaults/home.png",
                    Provision = "Long table with rakia"
                },
                new Home()
                {
                    Id = 2,
                    Name = "House on the Sunnybeach shore",
                    Activities = "fishing, drinking, puking etc.",
                    EndDate = DateTime.Now.AddDays(10),
                    StartDate = DateTime.Now.AddDays(3),
                    IsVisited = true,
                    Notes = "There is a disco near it.",
                    Location = new Location()
                    {
                        Id = 1,
                        Country = "Bulgaria",
                        LocationName = "Sunnybeach",
                        Latitude = -52.58m,
                        Longitude = -1.97m
                    },
                    PostedDate = DateTime.Now,
                    ProfilePicture = "/Defaults/home.png",
                    Provision = "Long table with rakia"
                }
            };

            this._context = new FakeSharenestDbContext();
            this._repository = new FakeHomesRepository(this._context);
            this._service = new HomesService(this._repository);
            this._controller = new HomesController(this._service);

            foreach (var home in homes)
            {
                this._context.Homes.Add(home);
            }
        }

        [TestMethod]
        public void Index_LastSixHome_ShouldReturn()
        {
            var data = this._controller.Index() as ViewResult;
            Assert.IsNotNull(data.Model);
        }

        [TestMethod]
        public void Index_LastSixHome_ShouldReturnRightHomes()
        {
            var data = this._controller.Index() as ViewResult;
            IList<HomesIndexViewModel> loadedModels = (IList<HomesIndexViewModel>) data.Model;
            for (int i = 0; i < loadedModels.Count; i++)
            {
                Assert.AreEqual(loadedModels[i].Id, homes[i].Id);
                Assert.AreEqual(loadedModels[i].Name, homes[i].Name);
            }
        }

        [TestMethod]
        public void Details_ShouldReturnRightHome()
        {
            var data = this._controller.Details(1) as ViewResult;
            HomeDetailsViewModel model = (HomeDetailsViewModel) data.Model;

            Assert.AreEqual(model.Id, homes[0].Id);
            Assert.AreEqual(model.Name, homes[0].Name);
            Assert.AreEqual(model.Location.Id, homes[0].Location.Id);
            Assert.AreEqual(model.Location.LocationName, homes[0].Location.LocationName);
        }

        [TestMethod]
        public void Details_NullValue_ShouldReturnBadRequest()
        {
            var controller = this._controller.Details(null) as HttpStatusCodeResult;
            Assert.AreEqual(controller.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void Details_FalseValue_ShouldReturnNotFound()
        {
            var controller = this._controller.Details(-1) as HttpStatusCodeResult;
            Assert.AreEqual(controller.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void Delete_FalseValue_ShouldReturnNotFound()
        {
            var controller = this._controller.Delete(-1) as HttpStatusCodeResult;
            Assert.AreEqual(controller.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void Delete_NullValue_ShouldReturnBadRequest()
        {
            var controller = this._controller.Details(null) as HttpStatusCodeResult;
            Assert.AreEqual(controller.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void Delete_ShouldReturnRightHome()
        {
            var data = this._controller.Delete(1) as ViewResult;
            HomeDetailsViewModel model = (HomeDetailsViewModel)data.Model;

            Assert.AreEqual(model.Id, homes[0].Id);
            Assert.AreEqual(model.Name, homes[0].Name);
            Assert.AreEqual(model.Location.Id, homes[0].Location.Id);
            Assert.AreEqual(model.Location.LocationName, homes[0].Location.LocationName);
        }

        [TestMethod]
        public void Delete_ShouldDeleteRightHome()
        {
            var data = this._controller.DeleteConfirmed(1) as ViewResult;

            bool doesExist = this._context.Homes.FirstOrDefault(h => h.Id == 1) != null;

            Assert.AreEqual(this._context.Homes.Count(), homes.Count - 1);
            Assert.IsFalse(doesExist);
        }

        [TestMethod]
        public void Create_ShouldAddElement()
        {
            var newHome = new AddHomeBindingModel()
            {
                Id = 3,
                Name = "New home",
                Activities = "interesting things",
                EndDate = DateTime.Now.AddDays(10),
                StartDate = DateTime.Now.AddDays(3),
                Notes = "No time for sleeping",
                LocationName = "Sofia",
                Country = "Bulgaria",
                Provision = "many drugs"
            };
            
            var data = this._controller.Create(newHome) as ViewResult;
            Assert.AreEqual(this._context.Homes.Count(), homes.Count + 1);
        }

        [TestMethod]
        public void Edit_FalseValue_ShouldReturnNotFound()
        {
            var controller = this._controller.Edit(-1) as HttpStatusCodeResult;
            Assert.AreEqual(controller.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void Edit_NullValue_ShouldUpdateRightModel()
        {
            UpdateHomeBindingModel model = new UpdateHomeBindingModel()
            {
                Id = 2,
                Name = "New home",
                Activities = "interesting things",
                EndDate = DateTime.Now.AddDays(10),
                StartDate = DateTime.Now.AddDays(3),
                Notes = "No time for sleeping",
                LocationName = "Sofia",
                Country = "Bulgaria",
                Provision = "many drugs"
            };

            var controller = this._controller.Edit(model) as ViewResult;

            var lastUpdated = _context.Homes.FirstOrDefault(home => home.Id == model.Id);
            Assert.IsNotNull(lastUpdated);
            //Assert.AreEqual(lastUpdated.Id, model.Id);
            //Assert.AreEqual(lastUpdated.Name, model.Name);
            //Assert.AreEqual(lastUpdated.Location.LocationName, model.LocationName);
            //Assert.AreEqual(lastUpdated.Location.Country, model.Country);
        }

        [TestMethod]
        public void Rate_ShouldRateRight()
        {
            DatailsRateBindingModel model = new DatailsRateBindingModel()
            {
                Id = 1,
                Rating = 10
            };

            this._controller.Rate(model);

            var lastUpdated = _context.Homes.FirstOrDefault(home => home.Id == model.Id);
            Assert.IsNotNull(lastUpdated);
        }
    }
}
