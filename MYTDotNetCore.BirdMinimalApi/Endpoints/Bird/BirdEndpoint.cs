using Newtonsoft.Json;

namespace MYTDotNetCore.BirdMinimalApi.Endpoints.Bird;

public static class BirdEndpoint
{
    public static void MapBirdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/birds", () =>
        {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(json);

            return Results.Ok(result);
        })
            .WithName("GetBirds")
            .WithOpenApi(); 

        app.MapGet("/birds/{id}", (int id) =>
        {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(json);
            var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
            return Results.Ok(item);
        })
            .WithName("GetBird")
            .WithOpenApi();

    }
}

