using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

using V2.Di.FunctionApp.Functions;
using V2.Di.FunctionApp.Functions.FunctionOptions;
using V2.Di.FunctionApp.Modules;

namespace V2.Di.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity to list all repositories for a given user or organisation from GitHub.
    /// </summary>
    public static class CoreGitHubRepositoriesHttpTrigger
    {
        public static IFunctionFactory Factory = new CoreFunctionFactory(new CoreAppModule());

        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns>Returns response.</returns>
        [FunctionName("CoreGitHubRepositoriesHttpTrigger")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "v2/repositories")]HttpRequest req, TraceWriter log)
        {
            var options = GetOptions(req);

            var result = await Factory.Create<IGitHubRepositoriesFunction>(log).InvokeAsync<HttpRequest, object>(req, options).ConfigureAwait(false);

            return new OkObjectResult(result);
        }

        private static GitHubRepositoriesHttpTriggerOptions GetOptions(HttpRequest req)
        {
            return new GitHubRepositoriesHttpTriggerOptions(req);
        }
    }
}