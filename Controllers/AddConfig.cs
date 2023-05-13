using Microsoft.Extensions.Configuration;

namespace LearnKeyVault.Controllers
{
    public class AddConfig
    {
        private readonly IConfiguration configuration;
            public AddConfig(IConfiguration configuration)
        {
            this.configuration = configuration;
                
        }
        public string StorageAccountName { get { return this.configuration["StorageAccountName"]; } }
        public string Key { get { return this.configuration["Key"]; } }
        public string Containername { get { return this.configuration["Containername"]; } }

        // Cosmos DB Config
        public string CosmosUrl { get { return this.configuration["CosmosUrl"]; } }
        public string CosmosDBName { get { return this.configuration["CosmosDBName"]; } }
        public string CosmosKey { get { return this.configuration["CosmosKey"]; } }
    }
}
