using System.Security.Claims;

namespace CosmosTrainingAPi.Services
{
    public interface IUserService
    {
        string parseNameFromToken();
        string[] parseRoleFromToken();
    }
}
