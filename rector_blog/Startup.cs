using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rector_blog.Startup))]
namespace rector_blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
