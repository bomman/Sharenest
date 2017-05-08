using Microsoft.AspNet.Identity;
using Sharenest.Models.EntityModels;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Sharenest.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Sharenest.App_Start.NinjectWebCommon), "Stop")]

namespace Sharenest.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Sharenest.Data.Interfaces;
    using Sharenest.Data;
    using Sharenest.Data.Repositories;
    using Sharenest.Services.Interfaces;
    using Sharenest.Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbContext>().To<SharenestDbContext>();

            kernel.Bind<IHomesRepository>().To<HomesRepository>();
            kernel.Bind<IPersonsRepository>().To<PersonsRepository>();

            kernel.Bind<IHomesService>().To<HomesService>();
            kernel.Bind<IAdminService>().To<AdminService>();
            kernel.Bind<IPersonsService>().To<PersonsService>();
        }        
    }
}
