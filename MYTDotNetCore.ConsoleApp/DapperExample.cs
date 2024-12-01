using System.Data;
using System.Data.SqlClient;
using Dapper;
using MYTDotNetCore.ConsoleApp.Models;

namespace MYTDotNetCore.ConsoleApp;

public class DapperExample
{
    private readonly string _connectionString = "Data Source=.; Initial Catalog=MYTDotNetCoreBatch5; User Id=sa; Password=sasa@123";

    public void Read()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0";
            var lst = db.Query<BlogDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }
    }

    public void Create(string title, string author, string content)
    {
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
                   ([BlogTitle]
                   ,[BlogAuthor]
                   ,[BlogContent]
                   ,[DeleteFlag])
             VALUES
                   (@BlogTitle
                   ,@BlogAuthor
                   ,@BlogContent
                   ,0)";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            // run -> query method when data get;
            // run -> execute method when data set; 
            var result = db.Execute(query, new { BlogTitle = title, BlogAuthor = author, BlogContent = content });
            Result("Blog Creation", result);
        }
    }

    public void Edit(int id)
    {
        string query = "SELECT * FROM tbl_blog WHERE DeleteFlag = 0 AND BlogId = @BlogId";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var lst = db.Query<BlogDataModel>(query, new { BlogId = id }).FirstOrDefault();
            if( lst is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            Console.WriteLine(lst.BlogTitle);
            Console.WriteLine(lst.BlogAuthor);
            Console.WriteLine(lst.BlogContent);
        }
    }

    public void Update(int id, string  title, string author, string content)
    {
        string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
                  ,[DeleteFlag] = 0
             WHERE BlogId = @BlogId";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var result = db.Execute(query, new {BlogId = id, BlogTitle =  title, BlogAuthor = author, BlogContent = content });
            Result("Updating", result);
        }
    }

    public void Delete(int id)
    {
        string query = "UPDATE tbl_blog SET DeleteFlag = 1 WHERE BlogId = @BlogId";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var result = db.Execute(query, new { BlogId = id });
            Result("Delete", result);
        }
    }

    public void Result(string message, int result)
    {
        Console.WriteLine(result > 0 ? $"{message} successful" : $"{message} failed");
    }
}
