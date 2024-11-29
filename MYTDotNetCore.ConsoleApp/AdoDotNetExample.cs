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

        
        void Result(int result)
        {
            Console.WriteLine(result > 0 ? "Operation Successful" : "Operation Failed");
        }
    }
}
