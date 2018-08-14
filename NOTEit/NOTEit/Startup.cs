using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NOTEit.Startup))]
namespace NOTEit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
