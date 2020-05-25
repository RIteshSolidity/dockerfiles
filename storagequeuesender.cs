using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;


namespace StorageQueueSender
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
            cq =  cqc.GetQueueReference(queuename);
            cq.CreateIfNotExists();

            string employeename = "ritesh";
            byte[] msgtext = System.Text.Encoding.ASCII.GetBytes(employeename);

            CloudQueueMessage msg = new CloudQueueMessage(msgtext);
            cq.AddMessage(msg);
            Console.WriteLine("Message sent !!");

        }
    }
}
