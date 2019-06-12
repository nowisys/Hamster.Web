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
    public class ServiceTest
    {
        public class Service
        {
            public string Data { get; set; }
        }

        public class DefaultServiceController : Controller
        {
            private Service service;

            public DefaultServiceController(Service service)
            {
                this.service = service;
            }

            public IActionResult Get()
            {
                return Ok(service.Data);
            }
        }

        [Route("data")]
        public class AttributeServiceController : Controller
        {
            private Service service;

            public AttributeServiceController(Service service)
            {
                this.service = service;
            }

            [HttpGet]
            public IActionResult Get()
            {
                return Ok(service.Data);
            }
        }

        private string WebRequest(string url)
        {
            using (var client = new HttpClient()) {
                var req = client.GetStringAsync(url);
                req.Wait();
                return req.Result;
            }
        }

        [Fact]
        public void DefaultGet()
        {
            var service = new Service();

            using (var plugin = new WebPlugin() { Name = "Hamster.Web" }) {
                plugin.Logger = new DebugLogger(plugin.Name);
                plugin.Settings = new WebPluginSettings() { Url = "http://localhost:8081/" };
                plugin.Init();
                plugin.AddApp("api", new WebService<Service>(service, typeof(DefaultServiceController)));
                plugin.Open();

                service.Data = "test1";
                Assert.Equal("test1", WebRequest("http://localhost:8081/api/DefaultService/Get"));

                service.Data = "test2";
                Assert.Equal("test2", WebRequest("http://localhost:8081/api/DefaultService/Get"));
            }
        }

        [Fact]
        public void AttributeGet()
        {
            var service = new Service();

            using (var plugin = new WebPlugin() { Name = "Hamster.Web" }) {
                plugin.Logger = new DebugLogger(plugin.Name);
                plugin.Settings = new WebPluginSettings() { Url = "http://localhost:8081/" };
                plugin.Init();
                plugin.AddApp("api", new WebService<Service>(service, typeof(AttributeServiceController)));
                plugin.Open();

                service.Data = "test1";
                Assert.Equal("test1", WebRequest("http://localhost:8081/api/data"));

                service.Data = "test2";
                Assert.Equal("test2", WebRequest("http://localhost:8081/api/data"));
            }
        }
    }
}
