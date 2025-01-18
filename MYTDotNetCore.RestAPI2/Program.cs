using Microsoft.AspNetCore.Mvc;

using Refit;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(n => new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!)
});
builder.Services.AddSingleton(n => new RestClient(builder.Configuration.GetSection("ApiDomainUrl").Value!));
builder.Services.AddRefitClient<ISnakeAPI>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/birds", async ([FromServices] HttpClient client) =>
{
    var response = await client.GetAsync("birds");
    return await response.Content.ReadAsStringAsync();
});

app.MapGet("/pick-a-pile", async ([FromServices] RestClient client) =>
{
    var request = new RestRequest("/pick-a-pile", Method.Get);
    var response = await client.GetAsync(request);
    return response.Content;
});

app.MapGet("/snakes", async ([FromServices] ISnakeAPI snakeApi) =>
{
    var response =  snakeApi.GetSnakes();
    return response;
});
app.Run();
public interface ISnakeAPI
{
    [Get("/snakes")]
    Task<List<SnakeModel>> GetSnakes();
};

public class SnakeModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string MMName { get; set; }
    public string EngName { get; set; }
    public string Detail { get; set; }
    public string IsPoison { get; set; }
    public string IsDanger { get; set; }
}

