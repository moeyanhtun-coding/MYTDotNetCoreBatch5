// See https://aka.ms/na

using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using MYTDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");
//string connectionString = "Data Source=.;Initial Catalog=MYTDotNetCoreBatch5;User ID=sa;" +
//    "Password=sasa@123;";
//SqlConnection connection = new SqlConnection(connectionString);
//connection.Open();
//Console.WriteLine("connection Open");


//string query = @"SELECT [BlogId]
//      ,[BlogTitle]
//      ,[BlogAuthor]
//      ,[BlogContent]
//      ,[DeleteFlag]
//  FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0";

//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);

//SqlDataReader reader = cmd.ExecuteReader();
//while (reader.Read())
//{
//    Console.WriteLine(reader["BlogTitle"]);
//    Console.WriteLine(reader["BlogAuthor"]);
//    Console.WriteLine(reader["BlogContent"]);
//}

//connection.Close();
//DataTable dt = new DataTable();
//adapter.Fill(dt);

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthor"]);
//    Console.WriteLine(dr["BlogContent"]);
//    Console.WriteLine(dr["DeleteFlag"]);
//}

//Console.WriteLine("connection Close");

//foreach ( DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthor"]);
//    Console.WriteLine(dr["BlogContent"]);
//    Console.WriteLine(dr["DeleteFlag"]);
//}

//string connectionString2 = "Data Source=.;Initial Catalog=MYTDotNetCoreBatch5; User ID =sa; Password = sasa@123;";
//SqlConnection sqlConnection2 = new SqlConnection(connectionString2);

//Console.WriteLine("Blog Title: ");
//string title = Console.ReadLine();
//Console.WriteLine("Blog Author: ");
//string author = Console.ReadLine();
//Console.WriteLine("Blog Content");
//string content = Console.ReadLine();

//sqlConnection2.Open();

//string query2 = @"INSERT INTO [dbo].[Tbl_Blog]
//           ([BlogTitle]
//           ,[BlogAuthor]
//           ,[BlogContent]
//           ,[DeleteFlag])
//     VALUES
//           (@BlogTitle
//           ,@BlogAuthor
//           ,@BlogContent
//           ,0)";

//SqlCommand cmd2 = new SqlCommand(query2, sqlConnection2);
//cmd2.Parameters.AddWithValue("@BlogTitle", title);
//cmd2.Parameters.AddWithValue("@BlogAuthor", author);
//cmd2.Parameters.AddWithValue("@BlogContent", content);

//var result = cmd2.ExecuteNonQuery();
//Console.WriteLine(result > 0 ? "Insert Successful" : "Insert Fail");

//string connectionString3 = "Data Source=.;Initial Catalog=MYTDotNetCoreBatch5; User ID =sa; Password = sasa@123;";
//SqlConnection sqlConnection3 = new SqlConnection(connectionString3);

//Console.WriteLine("Blog Title: ");
//string title = Console.ReadLine();

//string query3 = @"DELETE FROM [dbo].[Tbl_Blog]
//      WHERE BlogTitle = @BlogTitle";


//sqlConnection3.Open();
//SqlCommand cmd3 = new SqlCommand(query3, sqlConnection3);
//cmd3.Parameters.AddWithValue("@BlogTitle", title);

//int result = cmd3.ExecuteNonQuery();
//sqlConnection3.Close();
//Console.WriteLine(result > 0 ? "Delete Successful" : "Delete Fail");

//string connectionString4 = "Data Source=.;Initial Catalog=MYTDotNetCoreBatch5; User ID =sa; Password = sasa@123;";
//SqlConnection sqlConnection4 = new SqlConnection(connectionString4);

//Console.WriteLine("Enter Title: ");
//string title = Console.ReadLine();
//sqlConnection4.Open();
//string query4 = @"SELECT [BlogId]
//      ,[BlogTitle]
//      ,[BlogAuthor]
//      ,[BlogContent]
//      ,[DeleteFlag]
//  FROM [dbo].[Tbl_Blog]
//  WHERE BlogTitle = @BlogTitle;";
//SqlCommand cmd = new SqlCommand(query4, sqlConnection4);
//cmd.Parameters.AddWithValue("@BlogTitle", title);

//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//SqlDataReader reader = cmd.ExecuteReader();
//while (reader.Read())
//{
//    Console.WriteLine("Title: " + reader["BlogTitle"]);
//    Console.WriteLine("Author: " + reader["BlogAuthor"]);
//    Console.WriteLine("Content: " + reader["BlogContent"]);
//}
//sqlConnection4.Close();
//DataTable dt = new DataTable();
//adapter.Fill(dt);

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthor"]);
//    Console.WriteLine(dr["BlogContent"]);
//}

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Read();
// adoDotNetExample.Create();
// adoDotNetExample.Delete(2);
// adoDotNetExample.Edit(6);
// adoDotNetExample.Update();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Create();
//dapperExample.Edit(8);
//dapperExample.Result("testing" , 1 
//dapperExample.Update(8, "Update new update", "Update new one ", "Update new one");
//dapperExample.Delete(8);
//dapperExample.Edit(8E);
// AdoDotNetExample2 eFCoreExample2 = new AdoDotNetExample2();
// DapperExample2 dapperExample2 = new DapperExample2();
// dapperExample2.Read();
// dapperExample2.Edit(13);
// dapperExample2.Update(13, "Update", "Update", "Update");
// dapperExample2.Delete(13);

//eFCoreExample2.Create();
//eFCoreExample2.Read();
//eFCoreExample2.Update();
//eFCoreExample2.Edit();
//eFCoreExample.Create("this is new Efcore title", "this is new Efcore AUthor", "this is new efcore content");
//eFCoreExample.Edit(30);
//eFCoreExample.Update( 30, "Update Title", "", "update content");
//eFCoreExample.Edit(30);
//eFCoreExample.Edit(10);
//eFCoreExample.Delelte(10);

// var service = new ServiceCollection()
//     .AddScoped<AdoDotNetExample>()
//     .BuildServiceProvider();
// var adoDotNetExample = service.GetRequiredService<AdoDotNetExample>();
// adoDotNetExample.Read();

// var services = new ServiceCollection()
//     .AddScoped<AdoDotNetExample2>()
//     .BuildServiceProvider();
// var adoDotNetExample2 = services.GetRequiredService<AdoDotNetExample2>();
// adoDotNetExample2.Read();

var service = new ServiceCollection()
    .AddScoped<DapperExample2>()
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();
var dapperExample2 = service.GetRequiredService<DapperExample2>();
var efCoreExample = service.GetRequiredService<EFCoreExample>();
// efCoreExample.Read();
// efCoreExample.Edit(27);
// Console.ReadLine();

// var service2 = new ServiceCollection();
// service2.AddScoped<EFCoreExample>();
// service2.BuildServiceProvider();
//
// var efCoreExample = service.GetRequiredService<EFCoreExample>();
// efCoreExample.Read();

