using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYTDotNetCore.Database.Model;

namespace MYTDotNetCore.MinimalApi.Endpoints.Blog;

public static class BlogEndpoint
{
    public static void MapBlogEndPoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
            {
                var model = db.TblBlogs.ToList();
                return Results.Ok(model);
            })
            .WithName("GetBlogs")
            .WithOpenApi();

        app.MapPost("/blogs", ([FromServices] AppDbContext db, TblBlog blog) =>
            {
                db.Add(blog);
                db.SaveChanges();
                return Results.Ok(blog);
            })
            .WithName("CreateBlog")
            .WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] AppDbContext db,int id) =>
            {
            
                var model = db.TblBlogs
                    .AsNoTracking()
                    .Where(x => x.BlogId == id)
                    .FirstOrDefault();
                if (model is null)
                    return Results.NotFound("Blog Not Found");
                return Results.Ok(model);
            })
            .WithName("GetBlog")
            .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
            {
                var model = db.TblBlogs
                    .AsNoTracking()
                    .FirstOrDefault(x => x.BlogId == id)!;
                if (model is null)
                    return Results.NotFound("Blog Not Found");
                model.BlogTitle = blog.BlogTitle;
                model.BlogAuthor = blog.BlogAuthor;
                model.BlogContent = blog.BlogContent;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok(blog);
            })
            .WithName("UpdateBlog")
            .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
            {
                var model = db.TblBlogs.AsNoTracking()
                    .FirstOrDefault(x => x.BlogId == id)!;
                if (model is null)
                    return Results.NotFound("Blog Not Found");
                model.DeleteFlag = true;

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok(model);
            })
            .WithName("DeleteBlog")
            .WithOpenApi();
    }
}