using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

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
            [ServiceBus("%ServiceBusQueue%", EntityType = EntityType.Queue)] IAsyncCollector<BrokeredMessage> collector,
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

                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serialised), false))
                {
                    var message = new BrokeredMessage(stream)
                    {
                        ContentType = "application/json",
                        Properties = { { "sample", "key" } }
                    };

                    await collector.AddAsync(message).ConfigureAwait(false);
                }
            }
        }
    }
}
