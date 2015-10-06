using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebPrinter.Startup))]
namespace WebPrinter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
