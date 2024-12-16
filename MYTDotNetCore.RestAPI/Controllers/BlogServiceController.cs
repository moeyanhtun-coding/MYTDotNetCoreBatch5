using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYTDotNetCore.Database.Model;
using MYTDotNetCore.Domain.Feature.Blog;

namespace MYTDotNetCore.RestAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogServiceController : ControllerBase
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

    [HttpPost]
    public IActionResult CreateBlog(TblBlog reqModel)
    {
        var result = _blogService.CreateBlog(reqModel);
        if (result is 0) return BadRequest(new { message = "Blog Create Fail" });
        return Ok(new { message = "Creation Successful" });
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlog( int id, TblBlog reqModel)
    {
        var result = _blogService.UpdateBlog(id, reqModel);
        if (result is 0) return BadRequest(new { message = "Blog Updated Fail" });
        return Ok(new { message = "Blog Update Successful"});
    }

    [HttpDelete]
    public IActionResult DeleteBlog(int id)
    {
        var result = (_blogService.DeleteBlog(id));
        if (result is 0) return BadRequest(new { message = "Blog Delete Fail" });
        return Ok(new { message = "Blog Delete Successful" });
    }
}
