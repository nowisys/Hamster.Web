using Hamster.Plugin;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Xml;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hamster.Plugin.Configuration;

namespace Hamster.Web
{
    public class WebPlugin : AbstractPlugin<WebPluginSettings>, IBindable
    {
        public static IHostingEnvironment HostingEnvironment { get; set; }
        public static IConfiguration Configuration { get; set; }
        private IWebHost host;
        private List<IPlugin> bindedPlugins = new List<IPlugin>();

        public override void Configure(XmlElement element)
        {
            base.Configure(element);
        }

        public void Bind(string name, object instance)
        {
            if(instance is IPlugin)
            {
                bindedPlugins.Add((IPlugin)instance);
            }
        }

        public void BindingComplete()
        {
            if(bindedPlugins.Count > 0)
            {
                foreach(IPlugin plugin in bindedPlugins)
                {
                    Logger.Info($"Following Plugin was bound to Web API: {plugin.Name}");
                }
            }
            else
            {
                Logger.Warn("No Plugins were bound to configured Web API.");
            }
        }

        /// <summary>
        /// Setup WebHost, configure MVC and routes through Startup.
        /// ConfigureServices discovers additional Implementation-Classes from binded Plugins, which will be used in Controllers.
        /// </summary>
        public override void Init()
        {
            host = new WebHostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    HostingEnvironment = hostingContext.HostingEnvironment;
                    Configuration = config.Build();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddProvider(new LogProvider(Logger));
                })
                .UseKestrel()
                .UseUrls(Settings.Url)
                .ConfigureServices(services =>
                {
                    foreach(IPlugin plugin in bindedPlugins)
                    {
                        if(plugin.PluginServiceProvider != null)
                        {
                            foreach(KeyValuePair<Type, object> serviceMapping in plugin.PluginServiceProvider.GetImplementations())
                            {
                                services.AddSingleton(serviceMapping.Key, serviceMapping.Value);
                            }
                        }
                        else
                        {
                            Logger.Info($"No Services found in ServiceProvider of Plugin: {plugin.Name}");
                        }
                    }
                })
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
