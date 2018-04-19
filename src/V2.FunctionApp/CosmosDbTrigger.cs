using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using V2.FunctionApp;

namespace V1.FunctionApp
{
    public static class CosmosDbTrigger
    {
        [FunctionName("CosmosDbTrigger")]
        public static void Run(
            [CosmosDBTrigger(
                databaseName: "%CosmosDbDdatabaseName%",
                collectionName: "%CosmosDbCollectionName%",
                LeaseDatabaseName = "%CosmosDbDdatabaseName%",
                LeaseCollectionName = "%CosmosDbLeaseCollectionName%")] IReadOnlyList<Document> input,
            TraceWriter log)
        {
            if (input == null || !input.Any())
            {
                log.Info("No document found");
            }

            log.Info($"Count: {input.Count}");

            foreach (var document in input)
            {
                log.Info(JsonConvert.SerializeObject(document));
                log.Info(document.ToString());

                Product product = (dynamic)document;
                log.Info(JsonConvert.SerializeObject(product));
            }
        }
    }
}
