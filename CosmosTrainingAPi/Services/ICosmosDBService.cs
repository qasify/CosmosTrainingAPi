using PracticeMVCApplication.Models;

namespace PracticeMVCApplication.Services
{
    public interface ICosmosDBService
    {
        Task UpsertItemAsync(User user);
    }
}
