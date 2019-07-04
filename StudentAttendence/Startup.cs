using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentAttendence.Startup))]
namespace StudentAttendence
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
