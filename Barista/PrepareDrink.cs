using System;
using System.Threading.Tasks;
using CafeCommon.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Barista
{
    public class PrepareDrink
    {
        [FunctionName("PrepareDrink")]
        public async Task Run([ServiceBusTrigger("orders", Connection = "MessageConnectionString")]string queueItem, ILogger log)
        {
            var order = JsonConvert.DeserializeObject<Order>(queueItem);

            log.LogInformation($"Received order for : {order.Name}");
            
            await this.PrepareOrderedDrink(log, order);
            
            log.LogInformation($"Order Complete : {order.Name}");
        }

        private async Task PrepareOrderedDrink(ILogger log, Order order)
        {
            log.LogInformation($"Started preparing drink for {order.Name}");
            
            // Just wait for 2 minutes assuming this time is required for the Barista to prepare the drink.
            await Task.Delay(2*60*1000);

            log.LogInformation($"Finished preparing drink for {order.Name}");
        }
    }
}
