using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductCatalogManager.Startup))]
namespace ProductCatalogManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
