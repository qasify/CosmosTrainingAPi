using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Diagnostics;

namespace PracticeMVCApplication.Services
{
    public class CosmosDBService : ICosmosDBService
    {
        private Database _database;
        public CosmosDBService(CosmosClient client)
        {
            initDatabase(client,"testApp");
        }

        private async void initDatabase(CosmosClient client, string name)
        {
            // Database reference with creation if it does not already exist
            _database = await client.CreateDatabaseIfNotExistsAsync(
                id: name
            );
        }
        private async Task<Container> getContainer(string name)
        {
            Container container = await _database.CreateContainerIfNotExistsAsync(
                id: name,
                partitionKeyPath: "/gender", // not sure what this is
                throughput: 400
            );
            return container;
        }


        async Task<string> ICosmosDBService.CreateNewUser(Models.User user)
        {
            string responce;
            try
            {
                Container container = await getContainer("user");
                var x = await container.CreateItemAsync<Models.User>(
                   item: user,
                   partitionKey: new PartitionKey(user.Gender)
               );
                responce = x.StatusCode.ToString();
            }
            catch (Exception e)
            {
                responce = "Conflict";
            }
            return responce;

        }

        async Task<List<Models.User>> ICosmosDBService.GetAllusers()
        {
            Container container = await getContainer("user");
            var allusers = new List<Models.User>();

            var query = new QueryDefinition(
                query: "SELECT * FROM users"
            );

            using FeedIterator<Models.User> feed = container.GetItemQueryIterator<Models.User>(
                queryDefinition: query
            );

            while (feed.HasMoreResults)
            {
                FeedResponse<Models.User> response = await feed.ReadNextAsync();
                foreach (Models.User item in response)
                {
                    allusers.Add(item);
                }
            }
            return (allusers);
        }
    }
}
