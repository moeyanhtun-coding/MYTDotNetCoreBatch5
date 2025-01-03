using MYTDotNetCore.Database.Model;
using MYTDotNetCore.Domain.Model;

namespace MYTDotNetCore.Domain.Feature.Blog;

public interface IBlogService
{
    List<TblBlog> GetBlogs();
    TblBlog GetBlogs(int id);
    Task<Result<ResultBlogResponseModel>> CreateBlogResult(TblBlog reqModel);
    Task<BlogResponseModel> CreateBlog(TblBlog reqModel);
    int UpdateBlog(int id, TblBlog reqModel);
    int DeleteBlog(int id);
}