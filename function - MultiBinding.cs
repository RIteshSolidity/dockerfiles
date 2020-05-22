using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Azure.Documents;

namespace multibindings
{
    public class Order
    {

        public string OrderID { get; set; }
        public string Description { get; set; }
        public string Productid { get; set; }
        public int Quantity { get; set; }

    }

    public class OrderInventory
    {
        public string id { get; set; }
        public string Description { get; set; }
        public string Productid { get; set; }
        public int AvailableQuantity { get; set; }

    }

    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] Order req,
            [CosmosDB("inventory", "India1", ConnectionStringSetting ="cosmosconnectionstring", Id = "{Productid}", PartitionKey = "{Productid}")] OrderInventory inventory,
            [ServiceBus("%orderqueuename%", Connection = "sbconnection")] IAsyncCollector<dynamic> output,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            log.LogInformation(req.Description);
            string returnvalue = null;
            if (inventory.AvailableQuantity > req.Quantity)
            {
                returnvalue = "success";
               
            }
            else {
                returnvalue = "failure";
                
            }
            await output.AddAsync(returnvalue);

            string responseMessage = string.IsNullOrEmpty(returnvalue)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {returnvalue}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }


}
