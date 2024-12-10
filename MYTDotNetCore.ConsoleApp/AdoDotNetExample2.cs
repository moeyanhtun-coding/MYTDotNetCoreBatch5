﻿using MYTDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MYTDotNetCore.Shared.AdoDotNetService;

namespace MYTDotNetCore.ConsoleApp
{

    public class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=.; Initial Catalog=MYTDotNetCoreBatch5; User Id=sa; Password=sasa@123";
        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                         ,[BlogTitle]
                         ,[BlogAuthor]
                         ,[BlogContent]
                         ,[DeleteFlag]
                        FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0";
            DataTable dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }

        public void Edit()
        {
            Console.WriteLine("Blog Id: ");
            string id = Console.ReadLine();
            string query = @"SELECT [BlogId]
                         ,[BlogTitle]
                         ,[BlogAuthor]
                         ,[BlogContent]
                         ,[DeleteFlag]
                        FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0 AND BlogId = @BlogId";
            var dt = _adoDotNetService.Query(query, new SqlParameterModel("@BlogId", id));
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("There is no data found");
                return;
            }
            Console.WriteLine(dt.Rows[0]["BlogId"]);
            Console.WriteLine(dt.Rows[0]["BlogTitle"]);
            Console.WriteLine(dt.Rows[0]["BlogAuthor"]);
            Console.WriteLine(dt.Rows[0]["BlogContent"]);
        }

        public void Create()
        {
            Console.WriteLine("BlogTitle: ");
            string title = Console.ReadLine();
            Console.WriteLine("BlogAuthor: ");
            string author = Console.ReadLine(); 
            Console.WriteLine("BlogContent: ");
            string content = Console.ReadLine();
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
            int result = _adoDotNetService.Execute(query, new SqlParameterModel("@BlogTitle", title),
                new SqlParameterModel("@BlogAuthor", author), new SqlParameterModel("@BlogContent", content));
            Console.WriteLine(result > 0 ? "Creation Successful" : "Creation fail");
        }
    }
}