using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYTDotNetCore.Database.Model;

namespace MYTDotNetCore.RestAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _db.TblBlogs.AsNoTracking()
                .Where(x => x.DeleteFlag != true)
                .ToList();
            return Ok( new { data = lst});
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok(blog);
        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().
                FirstOrDefault(x => x.DeleteFlag != true && x.BlogId == id);
            return Ok(item);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking()
                .FirstOrDefault(x => x.DeleteFlag != true && x.BlogId == id);
            if(item is null)
            {
                return NotFound();
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpPatch]
        public IActionResult PatchBlog()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBlog() 
        {
            return Ok();
        }
    }
}
