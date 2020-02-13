using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeyondTheTutor.Startup))]
namespace BeyondTheTutor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
