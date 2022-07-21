using Microsoft.AspNetCore.Mvc;
using PracticeMVCApplication.Models;

namespace PracticeMVCApplication.Services
{
    public interface ICosmosDBService
    {
        Task<string> CreateNewUser(User user);
        Task<ActionResult<List<User>>> GetAllusers();
        Task<ActionResult<string>> createPost(Post post);
    }
}
