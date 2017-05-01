using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Sharenest.Models.BindingModels;
using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Homes;

namespace Sharenest
{
    public class MvcApplication : System.Web.HttpApplication
    {
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
                    .ForMember(dest => dest.Location, opt =>
                            opt.MapFrom(src => Mapper.Map<AddHomeBindingModel, Location>(src)));

                    cfg.CreateMap<UpdateHomeBindingModel, Location>();
                    cfg.CreateMap<UpdateHomeBindingModel, Home>()
                        .ForMember(dest => dest.Location, opt =>
                            opt.MapFrom(src => Mapper.Map<UpdateHomeBindingModel, Location>(src)));
                    //cfg.CreateMap<Home, UpdateHomeBindingModel>();
                    //cfg.CreateMap<AddHomeBindingModel, Home>()
                    //    .ForMember(x => x.Location.Name, y => y.MapFrom(src => src.LocationName))
                    //    .ForMember(x => x.Location.Country, y=> y.MapFrom(src => src.Country));
                    //cfg.CreateMap<UpdateHomeBindingModel, Home>()
                    //    .ForMember(x => x.Location.Name, y => y.MapFrom(src => src.LocationName))
                    //    .ForMember(x => x.Location.Country, y => y.MapFrom(src => src.Country)); 

                    #endregion

                    #region View Models

                    cfg.CreateMap<Home, HomeEditViewModel>()
                        .ForMember(x => x.PicturesMedium, y => y.MapFrom(src => src.Pictures.Select(item => item.MediumPicturePath)));
                    cfg.CreateMap<Home, HomeDetailsViewModel>()
                        .ForMember(x => x.PicturesMedium, y => y.MapFrom(src => src.Pictures.Select(item => item.MediumPicturePath)));
                    cfg.CreateMap<Home, HomesIndexViewModel>();

                    #endregion

                    cfg.CreateMap<HomeEditViewModel, UpdateHomeBindingModel>();
                }
            );
        }
    }
}
