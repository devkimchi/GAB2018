using System.Net.Http;

using Autofac;

using V1.Di.FunctionApp.Configs;
using V1.Di.FunctionApp.Functions;

namespace V1.Di.FunctionApp.Modules
{
    public class AutofacAppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var github = new GitHub();
            builder.RegisterInstance(github).As<GitHub>().SingleInstance();

            var httpClient = new HttpClient();
            builder.RegisterInstance(httpClient).As<HttpClient>().SingleInstance();

            builder.RegisterType<AutofacGitHubRepositoriesFunction>().As<IGitHubRepositoriesFunction>().InstancePerLifetimeScope();
        }
    }
}