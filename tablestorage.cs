using System;
using System.Linq;
using Microsoft.Azure.Cosmos.Table;

namespace TableStorageDemo
{

    public class employee: TableEntity {

        public employee()
        {

        }

        public employee(string city,  string employeeid)
        {
            PartitionKey = city ;
            RowKey = employeeid;
        }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
    class Program
    {

        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=deststorage13;AccountKey=1jlPjEzdXz51ZlloSGuDRAJpDjFxnxjPVGrZSOz75LQm0ld55Mc2GbJcVKg52HO04BZ1rbTQjJpbJ1Zee4WWOg==;EndpointSuffix=core.windows.net";

        private static string tableName = "EmployeeTable123";

        private static CloudStorageAccount csa;

        private static CloudTableClient ctc;

        private static CloudTable ct;
        static void Main(string[] args)
        {
            csa = CloudStorageAccount.Parse(connectionString);
            ctc = csa.CreateCloudTableClient();

            // get a reference of a table
            ct = ctc.GetTableReference(tableName);
            ct.CreateIfNotExists();

            InsertData();

            QueryAllRecords();

        }

        static void QueryAllRecords() {

            TableQuery<employee> tq = new TableQuery<employee>().Where(
                    TableQuery.CombineFilters(
                            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Hyderabad"),
                            TableOperators.And,
                            TableQuery.GenerateFilterCondition("firstname", QueryComparisons.Equal, "rrr")
                        )
                );

            TableQuerySegment<employee> tqs = ct.ExecuteQuerySegmented<employee>(tq, new TableContinuationToken());
  
            foreach(var data in tqs)
            {
                Console.WriteLine(data.firstname + " " + data.lastname + " " + data.PartitionKey);

            }
        }

        static void InsertData() {
            employee e1 = new employee("Mumbai", "E001") {
                firstname = "xyz",
                lastname = "abc"
            };

            TableOperation ops1 = TableOperation.Insert(e1);
            ct.Execute(ops1);

            employee e2 = new employee("Hyderabad", "E002")
            {
                firstname = "mmm",
                lastname = "nnn"
            };
            TableOperation ops2 = TableOperation.Insert(e2);

            

            employee e3 = new employee("Hyderabad", "E003")
            {
                firstname = "rrr",
                lastname = "ttt"
            };
            TableOperation ops3 = TableOperation.Insert(e3);


            TableBatchOperation tbo = new TableBatchOperation();
            tbo.Add(ops2);
            tbo.Add(ops3);
            ct.ExecuteBatch(tbo);

        }
    }
}
