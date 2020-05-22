using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace GetTopicMessages
{

    public class Order
    {
        public string OrderId { get; set; }
        public string OrderDescription { get; set; }
    }
    class Program
    {
        static string connectionString = "Endpoint=sb://samplesbthursday.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=y/ndVwIt4iYXolh75cM6q1QIIFV5TZj+1SpAWqJxvOU=";

        static SubscriptionClient client = new SubscriptionClient(connectionString, "testtopic","subscriber1");
        static void Main(string[] args)
        {
            //SubscriptionClient client = new SubscriptionClient(connectionString, "testtopic","subscriber1");

            MessageHandlerOptions options = new MessageHandlerOptions(exceptionHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            };

            client.RegisterMessageHandler(ProcessMessages, options);

            Console.ReadLine();
        }

        private static async Task ProcessMessages(Message arg1, CancellationToken arg2)
        {
            string s = System.Text.Encoding.ASCII.GetString(arg1.Body);
            Order json = JsonConvert.DeserializeObject<Order>(s);
            Console.WriteLine(json.OrderDescription);
            await client.CompleteAsync(arg1.SystemProperties.LockToken);
            await Task.FromResult(json);
        }

        private static async Task exceptionHandler(ExceptionReceivedEventArgs arg)
        {
            await Task.FromResult("none");
        }
    }
}
