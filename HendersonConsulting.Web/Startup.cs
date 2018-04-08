using HendersonConsulting.Web.Models;
using HendersonConsulting.Web.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HendersonConsulting.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            var config = builder.Build();

            var url = $"https://{config["azureKeyVault"]}.vault.azure.net/";
            var clientId = config["azureKeyVaultClientId"];
            var clientSecret = config["azureKeyVaultSecret"];

            builder.AddAzureKeyVault(url, clientId, clientSecret);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Includes support for Razor Pages and controllers.
            services.AddOptions();

            services.Configure<AppSettings>(Configuration.GetSection("appSettings"));
            services.AddMvc();

            // Register application services.
            services.AddScoped<IStorageRepository, StorageRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc();


        }
    }
}
