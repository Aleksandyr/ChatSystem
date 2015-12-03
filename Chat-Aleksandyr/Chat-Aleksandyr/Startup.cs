using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chat_Aleksandyr.Startup))]
namespace Chat_Aleksandyr
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
