using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecureCarts.Startup))]
namespace SecureCarts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
