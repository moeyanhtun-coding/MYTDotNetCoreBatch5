using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MYTDotNetCore.Database.Model;
using MYTDotNetCore.RestAPI.ViewModels;

namespace MYTDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString;

        public BlogsAdoDotNetController(IConfiguration configuration )
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lst.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                });
            }

            connection.Close();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            var item = new BlogViewModel();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
                   ,[BlogTitle]
                   ,[BlogAuthor]
                   ,[BlogContent]
                   ,[DeleteFlag]
                  FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            ;
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                item = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };
            }
            else
            {
                return NotFound();
            }

            return Ok(new { Blog = item });
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
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
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            var result = cmd.ExecuteNonQuery();
            return Ok(result > 0 ? "Blog Creation Successful" : "Blog Creation Fail");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
                  ,[DeleteFlag] = 0
             WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            return Ok(result > 0 ? "Updating Successful" : "Updating Fail");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string conditions = "";
            if (!string.IsNullOrEmpty(blog.Title))
                conditions += " [BlogTitle] = @BlogTitle, ";
            if (!string.IsNullOrEmpty(blog.Author))
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            if (!string.IsNullOrEmpty(blog.Content))
                conditions += " [BlogContent] = @BlogContent, ";

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
               SET {conditions} ,[DeleteFlag] = 0
             WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.Title))
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            if (!string.IsNullOrEmpty(blog.Author))
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            if (!string.IsNullOrEmpty(blog.Content))
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return Ok(result > 0 ? "Update Successful" : "Update Fail");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [DeleteFlag] = 1
             WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            return Ok(result > 0 ? "Delete Successful" : "Delete Fail");
        }
    }
}