using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MYTDotNetCore.ConsoleApp.Models;

namespace MYTDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=.; Initial Catalog=MYTDotNetCoreBatch5; User Id=sa; Password=sasa@123";

        public void Read() 
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0";
                var lst = db.Query<BlogDataModel>( query).ToList();
                foreach (var item in lst) {
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
                   ,0";
            using (IDbConnection db = new SqlConnection(_connectionString)) 
            {
                db.Execute(query);
            }
        }
    }
}
