using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EthumRamiAssiSignup.Startup))]
namespace EthumRamiAssiSignup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
