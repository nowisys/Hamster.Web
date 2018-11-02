using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hamster.Web
{
    public interface IWebApp
    {
        void ConfigureServices(IServiceCollection services);
        void Configure(IApplicationBuilder builder);
    }
}
