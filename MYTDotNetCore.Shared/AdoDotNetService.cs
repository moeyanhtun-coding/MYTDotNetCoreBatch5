using System.Data;
using System.Data.SqlClient;

namespace MYTDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (sqlParameters is not null)
            {
                foreach (var parameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Name, parameter.Value);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable td = new DataTable();
            adapter.Fill(td);

            connection.Close();

            return td;
        }

        public class SqlParameterModel
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public string V { get; }
            public string? Id { get; }

            public SqlParameterModel() { }
            public SqlParameterModel(string name, object value)
            {
                Name = name;
                Value = value;
            }
        }
    }
}