using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace SharePoint.Integration.ACS
{
    class Program
    {
        private static string HostingEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var configuration = LoadConfiguration();
            services.AddSingleton(configuration);

            var authConfig = configuration.Get<AuthenticationConfig>();
            services.AddSingleton(authConfig);

            services.AddTransient<App>();
            services.AddTransient<IContextFactory, ContextFactory>();
            services.AddTransient<IAzureAuthProvider, AzureAuthProvider>();
            services.AddTransient<ISharePointManager, SharePointManager>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (HostingEnvironment == "Development")
            {
                builder.AddUserSecrets<AuthenticationConfig>();
            }

            return builder.Build();
        }
    }
}
