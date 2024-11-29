using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;

namespace MYTDotNetCore.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=.; Initial Catalog=MYTDotNetCoreBatch5; User Id=sa; Password=sasa@123";
        public void Read()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            string query = @"SELECT [BlogId]
                         ,[BlogTitle]
                         ,[BlogAuthor]
                         ,[BlogContent]
                         ,[DeleteFlag]
                        FROM [dbo].[Tbl_Blog] WHERE DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            sqlConnection.Close();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }

        public void Create()
        {
            Console.WriteLine("Blog Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content: ");
            string content = Console.ReadLine();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
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
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            Result(result);
        }

        public void Edit(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            string query = @"SELECT [BlogId]
                   ,[BlogTitle]
                   ,[BlogAuthor]
                   ,[BlogContent]
                   ,[DeleteFlag]
                  FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            while (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
                return;
            }
            Console.WriteLine("Data Not Found");
            return;
        }

        public void Update()
        {
            SqlConnection sqlConnection = new SqlConnection( _connectionString);

            Console.WriteLine("BlogId:");
            string BlogId = Console.ReadLine();

            Console.WriteLine("BlogTitle: ");
            string BlogTitle = Console.ReadLine();

            Console.WriteLine("BlogAuthor: ");
            string BlogAuthor = Console.ReadLine();

            Console.WriteLine("BlogContent: ");
            string BlogContent = Console.ReadLine();

            sqlConnection.Open();
            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
                  ,[DeleteFlag] = 0
             WHERE BlogId = @BlogId";   

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", BlogId);
            cmd.Parameters.AddWithValue("@BlogTitle", BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", BlogContent);

            int result = cmd.ExecuteNonQuery();
            Result(result);
        }

        public void Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            Result(result);
        }

        void Result(int result)
        {
            Console.WriteLine(result > 0 ? "Operation Successful" : "Operation Failed");
        }
    }
}
