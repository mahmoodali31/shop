using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineShopping.Web.Startup))]
namespace OnlineShopping.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
