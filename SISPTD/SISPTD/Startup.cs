using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SISPTD.Startup))]
namespace SISPTD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
