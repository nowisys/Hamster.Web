using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Hamster.Plugin;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Hamster.Web
{
    public class WebPlugin : AbstractPlugin<WebPluginSettings>
    {
        private IWebHost host;

        public override void Configure(XmlElement element)
        {
            base.Configure(element);
        }

        /// <summary>
        /// Setup WebHost, configure MVC and routes through Startup.
        /// </summary>
        public override void Init()
        {
            host = new WebHostBuilder()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddProvider(new LogProvider(Logger));
                })
                .UseKestrel()
                .UseUrls(Settings.Url)
                .UseStartup<Startup>()
                .Build();
        }

        /// <summary>
        /// Start Web API.
        /// </summary>
        public override void Open()
        {
            host.Start();
        }

        public override void Close()
        {
            host.StopAsync();
        }
    }
}
