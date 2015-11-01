using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcConfig.Example.Web.Startup))]
namespace EcConfig.Example.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
