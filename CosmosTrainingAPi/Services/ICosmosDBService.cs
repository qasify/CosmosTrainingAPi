using Microsoft.AspNetCore.Mvc;
using CosmosTrainingAPi.Models;

namespace CosmosTrainingAPi.Services
{
    public interface ICosmosDBService
    {
        public Task<string> CreateNewUser(UserDTO userDto);
        public Task<List<Models.User>> GetAllusers();
        public Task<string> createPost(Post post);
        public Task<List<Models.Post>> GetAllposts();
        public Task<string> AuthenciateUser(Models.UserCredentials user);
        public Task<string> UpdateUserPassword(Models.UpdatePassword user);
        public Task<string> DeletePost(DeletePost post);
    }
}