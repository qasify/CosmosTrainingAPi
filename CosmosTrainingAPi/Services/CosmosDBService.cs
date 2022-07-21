using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using PracticeMVCApplication.Models;
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
                partitionKeyPath: "/id", // not sure what this is
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
                   partitionKey: new PartitionKey(user.Username)
               );
                responce = x.StatusCode.ToString();
            }
            catch (Exception e)
            {
                responce = "Conflict";
            }
            return responce;

        }

        async Task<ActionResult<List<Models.User>>> ICosmosDBService.GetAllusers()
        {
            Container container = await getContainer("user");

            List<Models.User> allusers = new List<Models.User>();
            using (FeedIterator<Models.User> resultSet = container.GetItemQueryIterator<Models.User>(
                queryDefinition: null,
                requestOptions: new QueryRequestOptions()
                {
                    PartitionKey = new PartitionKey("id")
                }))
            {
                while (resultSet.HasMoreResults)
                {
                    FeedResponse<Models.User> response = await resultSet.ReadNextAsync();
                    allusers.AddRange(response);
                }
            }
            return allusers;
        }

        public async Task<string> createPost(Post post)
        {
            string resp;
            try
            {
                Container container = await getContainer("post");
                var x = await container.CreateItemAsync<Models.Post>(
                   item: post,
                   partitionKey: new PartitionKey(post.Id)
               );
                resp = x.StatusCode.ToString();
            }
            catch (Exception e)
            {
                resp = e.Message;
            }

            return resp;
        }
    }
}
