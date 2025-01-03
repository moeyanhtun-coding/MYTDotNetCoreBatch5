﻿using Microsoft.EntityFrameworkCore;
using MYTDotNetCore.ConsoleApp.Models;

namespace MYTDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where(x => x.DeleteFlag == false).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }
        public void Create(string title, string author, string content)
        {
            var blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();
            Result(result);
        }

        public void Edit(int id)
        {

            AppDbContext db = new AppDbContext();
            var item = db.Blogs.Where<BlogDataModel>(x => x.BlogId == id && x.DeleteFlag == false).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No Data Found ");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        public void Update(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            if (!string.IsNullOrEmpty(title))
                item.BlogTitle = title;
            if (!string.IsNullOrEmpty(author))
                item.BlogAuthor = author;
            if (!string.IsNullOrEmpty(content))
                item.BlogContent = content;

            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1);
        }
        public void Delelte(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().Where(x => x.BlogId == id && x.DeleteFlag == false).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No Data found");
                return;
            }
            db.Entry(item).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Result(result);
        }
        public void Result(int result)
        {
            Console.WriteLine(result > 0 ? "Operation Successful" : "Operation Fail");
        }
    }
}
