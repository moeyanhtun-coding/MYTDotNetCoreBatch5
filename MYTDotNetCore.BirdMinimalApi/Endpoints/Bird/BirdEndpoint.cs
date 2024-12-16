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

        app.MapPost("/birds", ( string birdEnglish, string birdMyanmar, string description) => {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(json)!;
            var lst = result.Tbl_Bird.ToList();
            BirdModel bird = new BirdModel()
            {
                Id = lst.Max(x => x.Id) + 1,
                BirdMyanmarName = birdMyanmar,
                BirdEnglishName = birdEnglish,
                Description = description,
                ImagePath = ""
            };
            lst.Add(bird);
            return Results.Ok(lst);
        }); ;
    }
}

