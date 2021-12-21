using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
 
namespace CustomCounter
{
     public class Counter
    {
        [JsonProperty(PropertyName = "id")]
        public string Id {get; set;}
        
        [JsonProperty(PropertyName = "index")]
        public string index {get; set;}


        [JsonProperty(PropertyName = "Count")]
        public int Count {get;set;}

    }
    public static class ResumeCounterTrigger
    {
        [FunctionName("GetResumeCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous , "get", Route = null)] HttpRequest req,
                     [CosmosDB(
                    databaseName:"CloudResume", 
                    collectionName: "tblCloudResume",
                    ConnectionStringSetting = "democosmos_DOCUMENTDB", 
                    Id = "One", 
                    PartitionKey = "RecordCount")] Counter Readcounter,  
                    [CosmosDB(
                        databaseName:"CloudResume",
                        collectionName: "tblCloudResume",
                        ConnectionStringSetting = "democosmos_DOCUMENTDB",
                        Id = "One", 
                        PartitionKey = "RecordCount")] out Counter updatedCounter,            
            ILogger log)
        {
                log.LogInformation("Function Triggered Done.");
                updatedCounter = Readcounter;
                updatedCounter.Count += 1;
           var jsonToReturn = JsonConvert.SerializeObject(updatedCounter);
           return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
