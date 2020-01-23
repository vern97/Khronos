using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SwimMeetTracker.Startup))]
namespace SwimMeetTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
