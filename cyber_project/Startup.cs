using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(cyber_project.Startup))]

namespace cyber_project
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) {
            app.MapSignalR();
        }
    }
}