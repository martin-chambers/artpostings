using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnnChambersArt.Startup))]
namespace AnnChambersArt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
