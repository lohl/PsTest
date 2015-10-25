using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingTest.Startup))]
namespace CodingTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
