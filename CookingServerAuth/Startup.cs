using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CookingServerAuth.Startup))]
namespace CookingServerAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
