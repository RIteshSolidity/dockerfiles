using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;

namespace storageQueueReceiver
{
    class Program
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=deststorage13;AccountKey=1jlPjEzdXz51ZlloSGuDRAJpDjFxnxjPVGrZSOz75LQm0ld55Mc2GbJcVKg52HO04BZ1rbTQjJpbJ1Zee4WWOg==;EndpointSuffix=core.windows.net";
        private static CloudStorageAccount csa;
        private static CloudQueueClient cqc;
        private static CloudQueue cq;

        private static string queuename = "testqueue";
        static void Main(string[] args)
        {
            csa = CloudStorageAccount.Parse(connectionString);
            cqc = csa.CreateCloudQueueClient();
            cq = cqc.GetQueueReference(queuename);
            cq.FetchAttributes();
            Console.WriteLine(cq.ApproximateMessageCount.ToString());

            foreach (var data in cq.PeekMessages (10)) {
                Console.WriteLine(System.Text.Encoding.ASCII.GetString(data.AsBytes));
                cq.DeleteMessage(data);
                
            }
        }
    }
}
