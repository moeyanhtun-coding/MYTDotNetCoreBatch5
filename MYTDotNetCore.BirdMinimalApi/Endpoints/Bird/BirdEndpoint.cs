using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(json)! ;
            var lst = result.Tbl_Bird.ToList();
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

        app.MapPost("/birds", (BirdModel requestModel) => {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);

            var result = JsonConvert.DeserializeObject<BirdResponseModel>(json)!;
            var lst = result.Tbl_Bird.ToList();

            requestModel.Id = lst.Count == 0 ? 1 : lst.Max(selector: x => x.Id) + 1;
            lst.Add(requestModel);

            var updateJson = JsonConvert.SerializeObject(new {Tbl_Bird = lst}, Formatting.Indented);
            File.WriteAllText(folderPath, updateJson);

            return Results.Ok(lst);
        }); 

        app.MapPatch("/birds/{id}", (int id, BirdModel requestModel) =>
        {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(json)!;
            if (result is null || result.Tbl_Bird is null)
            {
                return Results.BadRequest(new { message = "Invalid data format in the JSON file." });
            }
            var lst = result.Tbl_Bird.ToList();
            var item = lst.Where(x => x.Id == id).FirstOrDefault();

            if (item is null)
            {
                return Results.BadRequest(new { message = "Item is not found" });
            }

            item.Id = id;
            if (!string.IsNullOrEmpty(requestModel.BirdEnglishName))
                item.BirdEnglishName = requestModel.BirdEnglishName;
            if (!string.IsNullOrEmpty(requestModel.BirdMyanmarName))
                item.BirdMyanmarName= requestModel.BirdMyanmarName ;
            if (!string.IsNullOrEmpty(requestModel.Description))
                item.Description = requestModel.Description;
            if (!string.IsNullOrEmpty(requestModel.ImagePath))
                item.ImagePath = requestModel.ImagePath;

            var updateJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText (folderPath, updateJson);

            return Results.Ok(new {message = "Update Successsful"});
        });

        app.MapDelete("/birds/{id}", (int id) => 
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText (folderPath);
            var result =JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

            var lst = result.Tbl_Bird.ToList();
            var item = lst.FirstOrDefault(x => x.Id == id)!;
            if (item is null) return Results.BadRequest(new { message = "Bird is not found" });
            result.Tbl_Bird.Remove(item);

            var updateStr = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText(folderPath, updateStr);
            return Results.Ok(new { message = "Bird Delete successful" });
        });

    }
}

