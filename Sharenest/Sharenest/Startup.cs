using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sharenest.Startup))]
namespace Sharenest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
