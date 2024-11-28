// See https://aka.ms/na

using System.Data;
using System.Data.SqlClient;

//Console.WriteLine("Hello, World!");
//string connectionString = "Data Source=.;Initial Catalog=MYTDotNetCoreBatch5;User ID=sa;" +
//    "Password=sasa@123;";
//SqlConnection connection = new SqlConnection(connectionString);
////connection.Open();
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

string connectionString2 = "Data Source=.;Initial Catalog=MYTDotNetCoreBatch5; User ID =sa; Password = sasa@123;";
SqlConnection sqlConnection2 = new SqlConnection(connectionString2);

Console.WriteLine("Blog Title: ");
string title = Console.ReadLine();
Console.WriteLine("Blog Author: ");
string author = Console.ReadLine();
Console.WriteLine("Blog Content");
string content = Console.ReadLine();

sqlConnection2.Open();

string query2 = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

SqlCommand cmd2 = new SqlCommand(query2, sqlConnection2);
cmd2.Parameters.AddWithValue("@BlogTitle", title);
cmd2.Parameters.AddWithValue("@BlogAuthor", author);
cmd2.Parameters.AddWithValue("@BlogContent", content);

var result = cmd2.ExecuteNonQuery();
Console.WriteLine(result > 0 ? "Insert Successful" : "Insert Fail");
