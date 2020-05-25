using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace BlobStorageDemo
{
    class Program
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=deststorage13;AccountKey=1jlPjEzdXz51ZlloSGuDRAJpDjFxnxjPVGrZSOz75LQm0ld55Mc2GbJcVKg52HO04BZ1rbTQjJpbJ1Zee4WWOg==;EndpointSuffix=core.windows.net";
        private static CloudStorageAccount csa;
        private static CloudBlobClient cbc;
        private static CloudBlobContainer container;
        static void Main(string[] args)
        {
            csa = CloudStorageAccount.Parse(connectionString);
            cbc = csa.CreateCloudBlobClient();
            container = cbc.GetContainerReference("newcontainer");

            container.CreateIfNotExists();

            CloudBlockBlob cbb = container.GetBlockBlobReference("ritesh");
            cbb.UploadFromFile(@"C:\Ritesh\aks\aks-first.yml");
        }
    }
}
