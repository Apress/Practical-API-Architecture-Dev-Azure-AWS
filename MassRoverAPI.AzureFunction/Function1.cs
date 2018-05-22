using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace MassRoverAPI.AzureFunction
{
    public static class GetProducts
    {
        [FunctionName("GetProducts")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest  req, TraceWriter log)
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Lithim L2"},
                new Product { Id = 2, Name = "SNU 61" }
            };

            var data = JsonConvert.SerializeObject(products);

            return (ActionResult)new OkObjectResult(data);
        }
    }


    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }

}
