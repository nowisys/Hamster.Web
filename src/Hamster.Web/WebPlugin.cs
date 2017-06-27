using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Hamster.Plugin;

namespace Hamster.Web
{
    public class WebPlugin : AbstractPlugin<WebPluginSettings>
    {
        private IWebHostBuilder builder;
        private IWebHost host;

        public override void Init()
        {
            builder = new WebHostBuilder()
                .ConfigureLogging(factory => {
                        factory.AddProvider(new LogProvider(Logger));
                    })
                .UseKestrel()
                .UseUrls(Settings.Url)
                .UseStartup<Startup>();
        }

        protected override void BaseOpen()
        {
            host = builder.Build();
            host.Start();
        }

        protected override void BaseClose()
        {
            host.Dispose();
        }
    }
}
