using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PosetiMe.Startup))]
namespace PosetiMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
