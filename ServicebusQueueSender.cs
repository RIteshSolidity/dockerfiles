using System;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace SenderServiceBus
{

    public class Order { 
        public string OrderId { get; set; }
        public string OrderDescription { get; set; }
    }
    class Program
    {
        static string connectionString = "Endpoint=sb://samplesbthursday.servicebus.windows.net/;SharedAccessKeyName=sendonly;SharedAccessKey=A9yj1vsgUkRBQXPgxL5GuXjx52z542CrWbz90qg6+1s=";
        static void Main(string[] args)
        {
            QueueClient client = new QueueClient(connectionString, "testqueue");

            Message message = new Message();

            Order o = new Order {
                 OrderDescription = "samsung phones",
                 OrderId = "A001"
            };


            string msg = JsonConvert.SerializeObject(o);

            message.Body = System.Text.Encoding.ASCII.GetBytes(msg);

            message.ContentType = "application/json";
            
            client.SendAsync(message).GetAwaiter().GetResult();

        }
    }
}
