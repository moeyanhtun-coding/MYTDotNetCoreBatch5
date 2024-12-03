using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            if(item is null)
            {
                Console.WriteLine("No Data Found");
                Console.WriteLine("No Data Found");
                return;
            }
            if (item is null)
                return;
            if (!string.IsNullOrEmpty(title))
                item.BlogTitle = title;
            if (!string.IsNullOrEmpty(author))
                item.BlogAuthor = author;
            if (!string.IsNullOrEmpty(content))
                item.BlogContent = content;

            db.Entry(item).State = EntityState.Modified;
            var resutl = db.SaveChanges();
            Console.WriteLine(resutl == 1);
        }

        public void Result(int result)
        {
            Console.WriteLine(result > 0 ? "Operation Successful" : "Operation Fail");
        }
    }
}
