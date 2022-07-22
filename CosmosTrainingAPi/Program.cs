using Microsoft.Azure.Cosmos;
using PracticeMVCApplication.Services;

// New instance of CosmosClient class
using CosmosClient client = new(
    accountEndpoint: "https://c2648450-0ee0-4-231-b9ee.documents.azure.com:443/",
    authKeyOrResourceToken: "UdPm9LEQTyoOftR2mieONaNucTmkdzFv0BxbQ9wvmH1zYxJJR2jvYu8jQlAFDIA34ZBTItAn0RqagMVHbaKV6A=="
);

var builder = WebApplication.CreateBuilder(args);


// adding cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "reactAppOrigin",
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICosmosDBService>(new CosmosDBService(client));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("reactAppOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
