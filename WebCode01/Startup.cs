using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebCode01.Startup))]
namespace WebCode01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
