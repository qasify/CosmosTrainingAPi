using Microsoft.Azure.Cosmos;

namespace PracticeMVCApplication.Services
{
    public class CosmosDBService : ICosmosDBService
    {
        Container container;
        public CosmosDBService(CosmosClient client)
        {
            getContainer(client);
        }

        private async void getContainer(CosmosClient client)
        {
            // Database reference with creation if it does not already exist
            Database database = await client.CreateDatabaseIfNotExistsAsync(
                id: "testApp"
            );

            container = await database.CreateContainerIfNotExistsAsync(
                id: "user",
                partitionKeyPath: "/id", // not sure what this is
                throughput: 400
            );
        }


        async Task ICosmosDBService.UpsertItemAsync(Models.User user)
        {
            await container.CreateItemAsync<Models.User>(
               item: user,
               partitionKey: new PartitionKey(user.Username)
           );
        }
    }
}
