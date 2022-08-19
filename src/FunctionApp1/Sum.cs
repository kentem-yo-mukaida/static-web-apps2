using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class Sum
    {
        [FunctionName("Sum")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (int.TryParse(req.Query["value1"], out var value1) &&
                int.TryParse(req.Query["value2"], out var value2))
            {
                return new OkObjectResult(new { value = value1 + value2 });
            }
            return new OkObjectResult(new { value = -1 });
        }
    }
}
