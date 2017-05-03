using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using AutoMapper;
using Sharenest.Models.BindingModels;
using Sharenest.Models.EntityModels;
using Sharenest.Models.ViewModels.Homes;
using System.Web;
using Sharenest.Models.ViewModels.Admin;

namespace Sharenest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string AppKey { get; private set; }

        public static string AppSecret { get; private set; }

        protected void Application_Start()
        {
            InitializeAppKeyAndSecret();

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

                    #endregion

                    cfg.CreateMap<HomeEditViewModel, UpdateHomeBindingModel>();
                }
            );
        }

        private void InitializeAppKeyAndSecret()
        {
            var appKey = WebConfigurationManager.AppSettings["DropboxAppKey"];
            var appSecret = WebConfigurationManager.AppSettings["DropboxAppSecret"];

            if (string.IsNullOrWhiteSpace(appKey) ||
                string.IsNullOrWhiteSpace(appSecret))
            {
                var infoPath = HttpContext.Current.Server.MapPath("~/App_Data/DropboxInfo.json");

                if (File.Exists(infoPath))
                {
                    string json;

                    using (var stream = new FileStream(infoPath, FileMode.Open, FileAccess.Read))
                    {
                        var reader = (TextReader)new StreamReader(stream);
                        json = reader.ReadToEnd();
                    }
                    var ser = new JavaScriptSerializer();
                    var info = ser.Deserialize<Dictionary<string, string>>(json);

                    appKey = info["AppKey"];
                    appSecret = info["AppSecret"];
                }
            }

            AppKey = appKey;
            AppSecret = appSecret;
        }
    }
}
