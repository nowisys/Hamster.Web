using System;
using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Hamster.Plugin;
using Hamster.Plugin.Debug;
using Hamster.Web;
using Hamster.Web.Test.Controllers;

namespace Hamster.Web.Test
{
    public class RouteTest
    {
        private string WebRequest(string url)
        {
            using (var client = new HttpClient()) {
                var req = client.GetStringAsync(url);
                req.Wait();
                return req.Result;
            }
        }

        [Fact]
        public void TwoApps()
        {
            using (var plugin = new WebPlugin() { Name = "Hamster.Web" }) {
                plugin.Logger = new DebugLogger(plugin.Name);
                plugin.Settings = new WebPluginSettings() { Url = "http://localhost:8080/" };
                plugin.Init();
                plugin.AddApp("app1",
                        services => {},
                        app => app.Run(async c => { await c.Response.WriteAsync("app1"); })
                        );
                plugin.AddApp("app2",
                        services => {},
                        app => app.Run(async c => { await c.Response.WriteAsync("app2"); })
                        );
                plugin.Open();

                Assert.Equal("app1", WebRequest("http://localhost:8080/app1"));
                Assert.Equal("app2", WebRequest("http://localhost:8080/app2"));
            }
        }

        [Fact]
        public void Mvc()
        {
            using (var plugin = new WebPlugin() { Name = "Hamster.Web" }) {
                plugin.Logger = new DebugLogger(plugin.Name);
                plugin.Settings = new WebPluginSettings() { Url = "http://localhost:8080/" };
                plugin.Init();
                plugin.AddApp("mvc",
                        services => {
                            services.AddMvc().AddApplicationPart(typeof(EchoController).Assembly);
                        },
                        app => {
                            app.UseMvc(routes => {
                                    routes.MapRoute(
                                            name: "default",
                                            template: "{controller=Echo}/{action=Echo}/{id?}"
                                            );
                                    });
                        });
                plugin.Open();

                Assert.Equal("foobar", WebRequest("http://localhost:8080/mvc"));
            }
        }
    }
}
