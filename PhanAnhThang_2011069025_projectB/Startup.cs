using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhanAnhThang_2011069025_projectB.Startup))]
namespace PhanAnhThang_2011069025_projectB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
