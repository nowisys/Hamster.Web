using Hamster.Plugin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using WebApiContrib.Core;

namespace Hamster.Web
{
    public class WebPlugin : AbstractPlugin<WebPluginSettings>
    {
        private class AppStartup
        {
            public Action<IServiceCollection> ConfigureServices { get; set; }
            public Action<IApplicationBuilder> Configure { get; set; }
        }

        private IWebHost host;
        private Dictionary<string, AppStartup> apps = new Dictionary<string, AppStartup>();

        public void AddApp(string name, IWebApp app)
        {
            apps.Add(name, new AppStartup() {
                    ConfigureServices=app.ConfigureServices,
                    Configure=app.Configure
                    });
        }

        public void AddApp(string name, Action<IServiceCollection> configureServices, Action<IApplicationBuilder> configure)
        {
            apps.Add(name, new AppStartup() {
                    ConfigureServices=configureServices,
                    Configure=configure
                    });
        }

        /// <summary>
        /// Setup WebHost, configure MVC and routes through Startup.
        /// ConfigureServices discovers additional Implementation-Classes from binded Plugins, which will be used in Controllers.
        /// </summary>
        public override void Init()
        {
        }

        private void ConfigureApps(IApplicationBuilder builder)
        {
            // https://www.strathweb.com/2017/04/running-multiple-independent-asp-net-core-pipelines-side-by-side-in-the-same-application/
            foreach (var entry in apps) {
                var app = entry.Value;
                var name = entry.Key;
                builder.UseBranchWithServices('/'+name, app.ConfigureServices, app.Configure);
            }
        }

        /// <summary>
        /// Start Web API.
        /// </summary>
        public override void Open()
        {
            var host = new WebHostBuilder()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddProvider(new LogProvider(Logger));
                    logging.AddDebug();
                })
                .UseKestrel()
                .UseUrls(Settings.Url)
                .Configure(ConfigureApps)
                .Build();

            host.Start();
            this.host = host;
        }

        public override void Close()
        {
            if (host != null) {
                host.StopAsync().Wait();
            }
        }
    }
}
