using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecurityTrial.Startup))]
namespace SecurityTrial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
