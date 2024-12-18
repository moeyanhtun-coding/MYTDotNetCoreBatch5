﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYTDotNetCore.Database.Model;

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

    public int CreateBlog(TblBlog reqModel)
    {
        _db.TblBlogs.Add(reqModel);
        var result = _db.SaveChanges();
        return result;
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
