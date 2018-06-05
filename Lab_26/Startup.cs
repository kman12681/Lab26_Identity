using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab_26.Startup))]
namespace Lab_26
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
