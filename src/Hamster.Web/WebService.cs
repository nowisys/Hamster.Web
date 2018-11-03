using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Hamster.Web
{
    public class WebService<TService> : IWebApp
        where TService : class
    {
        private TService service;
        private List<TypeInfo> controllers;

        public WebService(TService service, params Type[] controllers)
            : this(service, from c in controllers select c.GetTypeInfo())
        {
        }

        public WebService(TService service, IEnumerable<TypeInfo> controllers)
        {
            this.service = service;
            this.controllers = new List<TypeInfo>(controllers);
        }

        public WebService<TService> Add<T>()
        {
            this.controllers.Add(typeof(T).GetTypeInfo());
            return this;
        }

        protected virtual void ConfigureMvc(IMvcBuilder mvc)
        {
            mvc.ConfigureApplicationPartManager(apm => {
                    apm.FeatureProviders.Clear();
                    apm.FeatureProviders.Add(new ControllerProvider(controllers));
                    apm.ApplicationParts.Clear();
                    apm.ApplicationParts.Add(new TypeProvider(controllers));
                    });
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(service);
            ConfigureMvc(services.AddMvc());
        }

        protected virtual void BuildRoutes(IRouteBuilder routes)
        {
            routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
        }

        public virtual void Configure(IApplicationBuilder builder)
        {
            builder.UseMvc(BuildRoutes);
        }
    }
}
