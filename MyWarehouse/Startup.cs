using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWarehouse.Startup))]
namespace MyWarehouse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
