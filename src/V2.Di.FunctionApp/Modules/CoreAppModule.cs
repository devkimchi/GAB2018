using System.IO;
using System.Net.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using V2.Di.FunctionApp.Configs;
using V2.Di.FunctionApp.Extensions;
using V2.Di.FunctionApp.Functions;

namespace V2.Di.FunctionApp.Modules
{
    /// <summary>
    /// This represents the module entity for dependencies.
    /// </summary>
    public class CoreAppModule : Module
    {
        /// <inheritdoc />
        public override void Load(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("config.json")
                             .Build();
            var github = config.Get<GitHub>("github");

            services.AddSingleton(github);
            services.AddSingleton<HttpClient>();
            services.AddTransient<IGitHubRepositoriesFunction, CoreGitHubRepositoriesFunction>();
        }
    }
}