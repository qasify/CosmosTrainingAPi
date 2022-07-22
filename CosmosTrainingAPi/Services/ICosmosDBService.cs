using Microsoft.AspNetCore.Mvc;
using PracticeMVCApplication.Models;

namespace PracticeMVCApplication.Services
{
    public interface ICosmosDBService
    {
        public Task<string> CreateNewUser(Models.User user);
        public Task<List<Models.User>> GetAllusers();
        public Task<string> createPost(Post post);
        public Task<string> AuthenciateUser(UserCredentials user);
    }
}
