using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYTDotNetCore.Database.Model;
using MYTDotNetCore.Domain;
using MYTDotNetCore.Domain.Feature.Blog;
using MYTDotNetCore.Domain.Model;

namespace MYTDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : BaseController
    {
        private readonly BlogService _blogService = new BlogService();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _blogService.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            var item = _blogService.GetBlogs(id);
            if (item is null)
                return BadRequest(new { message = "Item not found" });
            return Ok(item);
        }

        [HttpPost("create-blog-result")]
        public async Task<IActionResult> CreateBlogResult(TblBlog blog)
        {
            try
            {
                var model = await _blogService.CreateBlogResult(blog);
                return Execute(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("create-blog")]
        public async Task<IActionResult> CreateBlog(TblBlog reqModel)
        {
            try
            {
                var model = await _blogService.CreateBlog(reqModel);
                return Execute(model);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog reqModel)
        {
            var result = _blogService.UpdateBlog(id, reqModel);
            if (result is 0) return BadRequest(new { message = "Blog Updated Fail" });
            return Ok(new { message = "Blog Update Successful" });
        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            var result = (_blogService.DeleteBlog(id));
            if (result is 0) return BadRequest(new { message = "Blog Delete Fail" });
            return Ok(new { message = "Blog Delete Successful" });
        }
    }
}