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
        private readonly string _connectionString = "Data Source=.; Initial Catalog = MYTDotNetCoreBatch5; User ID=sa; Password=sasa@123; TrustServerCertificate = true";

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
            return Ok(new { data = lst});
        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog) 
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            return Ok();
        }

    }
}
