using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

using V1.Di.FunctionApp.Functions;
using V1.Di.FunctionApp.Functions.FunctionOptions;
using V1.Di.FunctionApp.Modules;

namespace V1.Di.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity to list all repositories for a given user or organisation from GitHub.
    /// </summary>
    public static class AutofacGitHubRepositoriesHttpTrigger
    {
        public static IFunctionFactory Factory = new AutofacFunctionFactory(new AutofacAppModule());

        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns>Returns response.</returns>
        [FunctionName("AutofacGitHubRepositoriesHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/repositories")]HttpRequestMessage req, TraceWriter log)
        {
            var options = GetOptions(req);

            var result = await Factory.Create<IGitHubRepositoriesFunction>(log).InvokeAsync<HttpRequestMessage, object>(req, options).ConfigureAwait(false);

            return req.CreateResponse(HttpStatusCode.OK, result);
        }

        private static GitHubRepositoriesHttpTriggerOptions GetOptions(HttpRequestMessage req)
        {
            return new GitHubRepositoriesHttpTriggerOptions(req);
        }
    }
}