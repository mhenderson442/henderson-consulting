namespace HendersonConsulting.Web
{
    using HendersonConsulting.Web.Middleware;
    using HendersonConsulting.Web.Models;
    using HendersonConsulting.Web.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Application startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="env"><see cref="IHostingEnvironment"/> instance.</param>
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

        /// <summary>
        /// Gets <see cref="IConfigurationRoot" />.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Includes support for Razor Pages and controllers.
            services.AddOptions();

            services.Configure<AppSettings>(config: Configuration.GetSection("appSettings"));
            services.AddMvc();

            // Register application services.
            services.AddScoped<IStorageRepository, StorageRepository>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
        /// <param name="env"><see cref="IHostingEnvironment"/> instance.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc();

            app.Map(new PathString("/images"), a => a.UseBlobFileHandler());
        }
    }
}
