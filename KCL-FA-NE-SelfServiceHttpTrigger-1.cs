using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KCLWave.SelfServiceHTTPTrigger
{
    public static class KCL_FA_NE_SelfServiceHttpTrigger_1
    {
        [FunctionName("KCL_FA_NE_SelfServiceHttpTrigger_1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string listItemId = req.Query["listItemId"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            listItemId = listItemId ?? data?.listItemId;

            log.LogInformation($"New SharePoint List Item ID, {listItemId}");

            return listItemId != null
                ? (ActionResult)new OkObjectResult($"New SharePoint List Item ID , {listItemId}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
