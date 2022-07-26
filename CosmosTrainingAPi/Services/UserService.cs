using System.Security.Claims;

namespace CosmosTrainingAPi.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string parseNameFromToken()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
        public string[] parseRoleFromToken()
        {
            IEnumerable<Claim> result = null;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role);
            }
            return result.ToList().Select(x => x.Value).ToArray();
        }
    }
}
