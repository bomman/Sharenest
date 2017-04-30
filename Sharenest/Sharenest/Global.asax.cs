using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Sharenest.Data;
using Sharenest.Data.Interfaces;
using Sharenest.Data.Repositories;
using Sharenest.Services;
using Sharenest.Services.Interfaces;

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

            var ninjectKernel = new StandardKernel();
            ConfigureDependencies(ninjectKernel);
        }

        private void ConfigureDependencies(StandardKernel ninjectKernel)
        {
            ninjectKernel.Bind<IDbContext>().To<SharenestDbContext>();
            ninjectKernel.Bind<IHomesRepository>().To<HomesRepository>();
            ninjectKernel.Bind<IHomesService>().To<HomesService>();
        }
    }
}
