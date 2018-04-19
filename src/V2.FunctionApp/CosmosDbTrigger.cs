using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Newtonsoft.Json;
using V2.FunctionApp;

namespace V1.FunctionApp
{
    public static class CosmosDbTrigger
    {
        [FunctionName("CosmosDbTrigger")]
        public async static Task Run(
            [CosmosDBTrigger(
                databaseName: "%CosmosDbDdatabaseName%",
                collectionName: "%CosmosDbCollectionName%",
                LeaseDatabaseName = "%CosmosDbDdatabaseName%",
                LeaseCollectionName = "%CosmosDbLeaseCollectionName%")] IReadOnlyList<Document> input,
            [ServiceBus("%ServiceBusQueue%", EntityType = EntityType.Queue)] IAsyncCollector<Message> collector,
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
                var serialised = JsonConvert.SerializeObject(product);

                log.Info(serialised);

                var message = new Message(Encoding.UTF8.GetBytes(serialised))
                {
                    ContentType = "application/json",
                    UserProperties = { { "sample", "key" } }
                };

                await collector.AddAsync(message).ConfigureAwait(false);
            }
        }
    }
}
