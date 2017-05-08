using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Sharenest.Models.BindingModels;
using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Account;
using Sharenest.Models.ViewModels.Homes;
using Sharenest.Models.ViewModels.Admin;

namespace Sharenest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string AccessToken { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureAutoMapper();
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(
                cfg =>
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


                    cfg.CreateMap<RegisterViewModel, Person>();

                    cfg.CreateMap<Person, AdminPersonsViewModel>();
                    #endregion

                    cfg.CreateMap<UpdateHomeBindingModel, Location>();
                    cfg.CreateMap<UpdateHomeBindingModel, HomeEditViewModel>()
                        .ForMember(dest => dest.Location, opt =>
                            opt.MapFrom(src => Mapper.Map<UpdateHomeBindingModel, Location>(src)));
                }
            );
        }
    }
}
