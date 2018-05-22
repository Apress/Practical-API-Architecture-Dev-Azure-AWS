using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace MassRoverAPI.AWSLambda
{
    public class Function
    {
        public string FunctionHandler(ILambdaContext context)
        {
            List<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Lithim L2" },
                new Product { Id = 2, Name = "SNU 61" }
            };

            var data = JsonConvert.SerializeObject(products);
            return data;
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
