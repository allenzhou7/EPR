using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_EF_SQLSERVE.Startup))]
namespace MVC_EF_SQLSERVE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
