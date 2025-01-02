namespace MYTDotNetCore.ConsoleApp3;

public interface IBlogApi
{
    [Get("/api/blogs")]
    Task<List<BlogModel>> GetBlogs();
    
    [Get("/api/blogs/{id}")]
    Task<BlogModel> GetBlogById(int id);
    
    [Post("/api/blogs")]
    Task<BlogModel> AddBlog(BlogModel blogModel);
    
    [Patch("/api/blogs/{id}")]
    Task<BlogModel> UpdateBlog(int id, BlogModel blogModel);
    
    [Delete("/api/blogs/{id}")]
    Task<BlogModel> DeleteBlog(int id);
}

public class BlogModel
{
    public int Id { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
    public bool DeleteFlag { get; set; }
}