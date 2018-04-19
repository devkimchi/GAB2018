
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace V2.FunctionApp
{
    /// <summary>
    /// This represents the trigger entity for HTTP request.
    /// </summary>
    public static class TestCosmosDbBinding
    {
        /// <summary>
        /// Invokes the trigger.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="collector"><see cref="IAsyncCollector{Product}"/> instance.</param>
        /// <param name="log"><see cref="TraceWriter"/> instance.</param>
        /// <returns>Returns the <see cref="IActionResult"/> instance.</returns>
        [FunctionName("TestCosmosDbBinding")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v2/products")] HttpRequest req,
            [CosmosDB(
                databaseName: "%CosmosDbDdatabaseName%",
                collectionName: "%CosmosDbCollectionName%")] IAsyncCollector<Product> collector,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var data = JsonConvert.DeserializeObject<Product>(await req.ReadAsStringAsync().ConfigureAwait(false));

            await collector.AddAsync(data).ConfigureAwait(false);

            return new OkObjectResult(data);
        }
    }
}
