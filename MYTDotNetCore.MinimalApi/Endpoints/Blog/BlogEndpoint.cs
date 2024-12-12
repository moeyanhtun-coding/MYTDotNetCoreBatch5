using Microsoft.EntityFrameworkCore;
using MYTDotNetCore.Database.Model;

namespace MYTDotNetCore.MinimalApi.Endpoints.Blog;

public static class BlogEndpoint
{
    public static void MapBlogEndPoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            AppDbContext db = new AppDbContext();
            var model = db.TblBlogs.ToList();
            return Results.Ok(model);
        })
            .WithName("GetBlogs")
            .WithOpenApi();

        app.MapPost("/blogs", (TblBlog blog) =>
        {
            AppDbContext db = new AppDbContext();
            db.Add(blog);
            db.SaveChanges();
            return Results.Ok(blog);
        })
            .WithName("CreateBlog")
            .WithOpenApi();

        app.MapGet("/blogs/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
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

        app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
        {
            AppDbContext db = new AppDbContext();
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

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
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
