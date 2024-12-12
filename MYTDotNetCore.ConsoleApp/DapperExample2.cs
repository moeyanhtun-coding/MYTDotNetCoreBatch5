using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MYTDotNetCore.ConsoleApp.Models;
using MYTDotNetCore.Shared;

namespace MYTDotNetCore.ConsoleApp
{
    public class DapperExample2
    {
        private readonly string _connectionString = "Data Source=.; Initial Catalog=MYTDotNetCoreBatch5; User Id=sa; Password=sasa@123";
        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {
            string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0";
            var lst = _dapperService.Query<BlogDapperDataModel>(query);
            foreach (var item in lst)
            {
                Console.WriteLine("BlogId: " + item.BlogId);
                Console.WriteLine("BlogTitle: " + item.BlogTitle);
                Console.WriteLine("BlogAuthor: " + item.BlogAuthor);
                Console.WriteLine("BlogContent: " + item.BlogContent);
            }
        }
        public void Edit(int id)
        {
            string query = @"SELECT [BlogId]
                         ,[BlogTitle]
                         ,[BlogAuthor]
                         ,[BlogContent]
                         ,[DeleteFlag]
                        FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0 AND BlogId = @BlogId";
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, new { BlogId = id });
            if(item is null){
                Console.WriteLine("Item Not Found");
                return;
            }
            Console.WriteLine("BlogId: " + item.BlogId);
            Console.WriteLine("BlogTitle: " + item.BlogTitle);
            Console.WriteLine("BlogAuthor: " + item.BlogAuthor);
            Console.WriteLine("BlogContent: " + item.BlogContent);
        }
        public void Update(int id, string title, string author, string content )
        {
              string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
                  ,[DeleteFlag] = 0
             WHERE BlogId = @BlogId and DeleteFlag = 0";
                 var result = _dapperService.Execute(query, new { BlogId = id, BlogTitle = title , BlogAuthor = author, BlogContent = content});
            Console.WriteLine(result);
        }
        public void Delete(int id) 
        {
          string query = "UPDATE tbl_blog SET DeleteFlag = 1 WHERE BlogId = @BlogId";
          var result =  _dapperService.Execute(query, new { BlogId = id });
            Console.WriteLine(result);
        }
    }
}
