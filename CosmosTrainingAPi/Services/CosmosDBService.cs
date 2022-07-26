using CosmosTrainingAPi.Models;
using Microsoft.Azure.Cosmos;
using System.Security.Claims;
using System.Security.Cryptography;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CosmosTrainingAPi.Services

{
    public class CosmosDBService : ICosmosDBService
    {
        private Database _database;
        private readonly IConfiguration _configuration;
        public CosmosDBService(CosmosClient client, IConfiguration configuration)
        {
            _configuration = configuration;
            initDatabase(client, "testApp");
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
            string path;
            if (name == "post")
                path = "/username";
            else
                path = "/id";
            Container container = await _database.CreateContainerIfNotExistsAsync(
                id: name,
                partitionKeyPath: path, // not sure what this is
                throughput: 400
            );
            return container;
        }

        public async Task<List<Models.User>> GetAllusers()
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

        public async Task<string> createPost(Post post)
        {
            string resp;
            try
            {
                Container container = await getContainer("post");
                post.Id = Guid.NewGuid().ToString();
                var x = await container.CreateItemAsync<Models.Post>(
                   item: post,
                   partitionKey: new PartitionKey(post.Username)
               );
                resp = x.StatusCode.ToString();
            }
            catch (Exception e)
            {
                resp = e.Message;
            }

            return resp;
        }

        public async Task<List<Models.Post>> GetAllposts()
        {
            Container container = await getContainer("post");
            var allPosts = new List<Models.Post>();

            var query = new QueryDefinition(
                query: "SELECT * FROM post"
            );

            using FeedIterator<Models.Post> feed = container.GetItemQueryIterator<Models.Post>(
                queryDefinition: query
            );

            while (feed.HasMoreResults)
            {
                FeedResponse<Models.Post> response = await feed.ReadNextAsync();
                foreach (Models.Post item in response)
                {
                    allPosts.Add(item);
                }
            }
            return (allPosts);
        }


        public async Task<string> UpdateUserPassword(Models.UpdatePassword user)
        {
            string responce;
            try
            {
                Container container = await getContainer("user");
                PatchItemRequestOptions options = new()
                {
                    FilterPredicate = $"FROM users u WHERE u.assword = \"{user.OldPassword}\""
                };
                List<PatchOperation> operations = new()
                {
                    PatchOperation.Replace("/Password", user.NewPassword)
                };
                var x = await container.PatchItemAsync<Models.UserDTO>(
                    id: user.Username,
                    partitionKey: new PartitionKey(user.Username),
                    patchOperations: operations,
                    requestOptions: options
                );

                responce = x.StatusCode.ToString();
            }
            catch (Exception e)
            {
                responce = "UserName or OldPassword didnt match.";
            }
            return responce;
        }

        public async Task<string> DeletePost(DeletePost post)
        {

            string resp;
            try
            {
                Container container = await getContainer("post");

                var x = await container.DeleteItemAsync<Models.Post>(
                   id: post.Id,
                   partitionKey: new PartitionKey(post.Username)
               );
                resp = x.StatusCode.ToString();
            }
            catch (Exception e)
            {
                resp = e.Message;
            }

            return resp;
        }

        public async Task<string> CreateNewUser(Models.UserDTO userDto)
        {
            string responce;
            try
            {
                CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                Models.User user = new Models.User
                {
                    Username = userDto.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Gender = userDto.Gender,
                };

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

        public async Task<string> AuthenciateUser(Models.UserCredentials user)
        {
            string responce;
            try
            {
                Container container = await getContainer("user");

                Models.User foundUser = await container.ReadItemAsync<Models.User>(
                    id: user.Username,
                    partitionKey: new PartitionKey(user.Username)
                );

                if (!VerifyPasswordHash(user.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
                {
                    responce = "Password incorrect";
                    return responce;
                }

                string token = CreateToken(foundUser);

/*                var refreshToken = GenerateRefreshToken();
                SetRefreshToken(refreshToken);*/

                responce = token;
            }
            catch (Exception e)
            {
                responce = "User don't exist";
            }
            return responce;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Models.User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /*private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }*/
    }
}
