using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using LearnKeyVault.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;


namespace LearnKeyVault.Controllers
{

    public class CosmosController : Controller
    {
        private readonly AddConfig config;
        private readonly ILogger _logger;

        public CosmosController(AddConfig config, ILogger<CosmosController> logger)
        {
            this.config = config;
            _logger = logger;

        }

        private Container ContainerClient() {
                CosmosClient cosmosDbClient = new CosmosClient(config.CosmosUrl, config.CosmosKey);
                Container containerClient = cosmosDbClient.GetContainer(config.CosmosDBName, "mynewdb");
                return containerClient;
            }
       
        [HttpGet("Cosmos")]
        public async Task<IActionResult> Index()
        {
            
           return View();

        }

        [HttpPost("Cosmos")]
        public async Task<IActionResult> Index(ItemModel items)
        {
            await CreateItem(items);

            return Ok("Successfully Inserted");
        }
        private async Task CreateItem(ItemModel item)
        {
            
            CosmosClient client = new CosmosClient(config.CosmosUrl, config.CosmosKey);
            Database database = await client.CreateDatabaseIfNotExistsAsync(config.CosmosDBName);
            Container container = await database.CreateContainerIfNotExistsAsync("mynewcontainer","/city",400);

            var test = new 
            {
            id = Guid.NewGuid().ToString(),
            Name = item.Name,
            Address = item.Address,
            City = item.City
            };

            var response = await container.CreateItemAsync(test);
        }

    }
}
