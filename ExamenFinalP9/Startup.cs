using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExamenFinalP9.Startup))]
namespace ExamenFinalP9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
