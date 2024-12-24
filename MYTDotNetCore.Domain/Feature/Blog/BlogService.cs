using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYTDotNetCore.Database.Model;
using MYTDotNetCore.Domain.Model;

namespace MYTDotNetCore.Domain.Feature.Blog;


public class BlogService
{
    private readonly AppDbContext _db = new AppDbContext();
    public List<TblBlog> GetBlogs()
    {
        var lst = _db.TblBlogs.ToList();
        return lst;
    }

    public TblBlog GetBlogs(int id)
    {
        
        var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        
        return item;
    }

    public async Task<Result<ResultBlogResponseModel>> CreateBlogResult(TblBlog reqModel)
    {
        Result<ResultBlogResponseModel> model = new  Result<ResultBlogResponseModel>();

        if (string.IsNullOrEmpty(reqModel.BlogTitle))
        {
            model = Result<ResultBlogResponseModel>.ValidationError("BlogTitle is required");
            goto result;
        }

        if (string.IsNullOrEmpty(reqModel.BlogAuthor))
        {
            model =Result<ResultBlogResponseModel>.ValidationError("Blog Author is required");
            goto result;
        }

        if (string.IsNullOrEmpty(reqModel.BlogContent))
        {
            model = Result<ResultBlogResponseModel>.ValidationError("Blog Content is required");
            goto result;
        }
        
        _db.TblBlogs.Add(reqModel);
        var result = await _db.SaveChangesAsync();
        ResultBlogResponseModel item = new ResultBlogResponseModel()
        {
            Blog = reqModel,
        };
        if (result > 0)
        {
            model = Result<ResultBlogResponseModel>.Success( item, "Blog Created");
        }
        result:
        return  model;
    }
    public async Task<BlogResponseModel> CreateBlog(TblBlog reqModel)
    {
        BlogResponseModel model = new  BlogResponseModel();

        if (string.IsNullOrEmpty(reqModel.BlogTitle))
        {
            model.Response = BaseResponseModel.ValidationError("400", "Blog Title is required");
            goto result;
        }

        if (string.IsNullOrEmpty(reqModel.BlogAuthor))
        {
            model.Response = BaseResponseModel.ValidationError("400", "Blog Author is required");
            goto result;
        }

        if (string.IsNullOrEmpty(reqModel.BlogContent))
        {
            model.Response = BaseResponseModel.ValidationError("400", "Blog Content is required");
            goto result;
        }
        
        _db.TblBlogs.Add(reqModel);
        var result = await _db.SaveChangesAsync();
        if (result > 0)
        {
            model.Response = BaseResponseModel.Success("200", "Blog Created");
        }
        result:
        return  model;
    }

    public int UpdateBlog(int id, TblBlog reqModel)
    {
        var item = _db.TblBlogs.AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id);

        if (item is null)
            return 0;

        item.BlogId = id;
        if (!string.IsNullOrEmpty(reqModel.BlogTitle))
            item.BlogTitle = reqModel.BlogTitle;
        if (!string.IsNullOrEmpty(reqModel.BlogAuthor))
            item.BlogAuthor = reqModel.BlogAuthor;
        if (!string.IsNullOrEmpty(reqModel.BlogContent))
            item.BlogContent = reqModel.BlogContent;
        _db.Entry(item).State = EntityState.Modified;

        var result = _db.SaveChanges();
        return result;
    }

    public int DeleteBlog(int id)
    {
        var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        if (item is null) return 0;
        item.DeleteFlag = true;

        _db.Entry(item).State = EntityState.Modified;
        var result = _db.SaveChanges();
        return result;
    }
}
