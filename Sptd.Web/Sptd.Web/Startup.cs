using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sptd.Web.Startup))]
namespace Sptd.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
