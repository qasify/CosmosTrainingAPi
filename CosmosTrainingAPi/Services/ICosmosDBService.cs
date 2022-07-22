using Microsoft.AspNetCore.Mvc;
using PracticeMVCApplication.Models;

namespace PracticeMVCApplication.Services
{
    public interface ICosmosDBService
    {
        Task<string> CreateNewUser(User user);
        Task<List<User>> GetAllusers();
        Task<string> createPost(Post post);
    }
}
