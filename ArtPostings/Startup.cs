﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtPostings.Startup))]
namespace ArtPostings
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
