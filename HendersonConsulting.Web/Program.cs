namespace HendersonConsulting.Web
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Initial application class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point for application.
        /// </summary>
        /// <param name="args">args string[] instance.</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builder method for <see cref="IWebHost"/>.
        /// </summary>
        /// <param name="args">args string[] instance.</param>
        /// <returns>Returns <see cref="IWebHost"/></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
