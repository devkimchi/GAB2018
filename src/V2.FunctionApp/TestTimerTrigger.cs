using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace V2.FunctionApp
{
    public static class TestTimerTrigger
    {
        [FunctionName("TestTimerTrigger")]
        public static void Run([TimerTrigger("0/10 * * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
