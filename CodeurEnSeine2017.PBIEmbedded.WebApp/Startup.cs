using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CodeurEnSeine2017.PBIEmbeded.WebApp.Startup))]

namespace CodeurEnSeine2017.PBIEmbeded.WebApp
{


    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
