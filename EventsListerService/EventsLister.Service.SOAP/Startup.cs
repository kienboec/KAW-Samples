using System.ServiceModel;
using EventsLister.CBP;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SoapCore;

namespace EventsLister.Service.SOAP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IArgumentHandler>((serviceProvider) => new ArgumentHandler(new string[] { }));
            services.AddSingleton<INetworkCommunicationHandler>((serviceProvider) => new NetworkCommunicationHandler());
            services.AddSingleton<IHTTPOutputInterpreter>((serviceProvider) => new HTTPOutputInterpreter());

            // https://github.com/DigDes/SoapCore
            services.TryAddSingleton<IFHTWService, FHTWService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // https://dottutorials.net/creating-soap-web-services-dot-net-core-tutorial/ // Attention: error in sample at using UseSoapEndpoint
            // https://devblogs.microsoft.com/dotnet/custom-asp-net-core-middleware-example/
            app.UseSoapEndpoint<IFHTWService>("/Service.asmx", new BasicHttpBinding());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
