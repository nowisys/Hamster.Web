using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Hamster.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add further services for the Web Plugin.
        public void ConfigureServices(IServiceCollection services)
        {
            // Discover Controller in Assemblies and add them to ASP.NET IoC.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where<Assembly>(a => a.GetName().FullName.Contains("Hamster")).ToArray();
            foreach (var assembly in assemblies)
            {
                services.AddMvc().AddApplicationPart(assembly).AddControllersAsServices();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
