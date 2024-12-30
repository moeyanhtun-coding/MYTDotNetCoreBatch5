namespace MYTDotNetCore.ConsoleApp3;

public class RefitExample
{
    public async Task RunAsync()
    {
        var blogApi = RestService.For<IBlogApi>("https://localhost:7274");
        var lst = await blogApi.GetBlogs();
        foreach (var item in lst)
        {
            Console.WriteLine(item.BlogTitle);
        }

        // var item2 = await blogApi.GetBlogById(20);
        // try
        // {
        //     var item3 = await blogApi.GetBlogById(30);
        // }
        // catch (ApiException e)
        // {
        //     if (e.StatusCode == HttpStatusCode.NotFound)
        //     {
        //         Console.WriteLine("Not Found");
        //     }
        // }

        var response = await blogApi.AddBlog(new BlogModel
        {
            BlogTitle = "I Love You",
            BlogAuthor = "Honey",
            BlogContent = "Babe",
        });
        Console.WriteLine(response.ToString());
    }
}