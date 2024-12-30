using System.Threading.Channels;

namespace MYTDotNetCore.ConsoleApp3;

public class RestClientExample
{
    private readonly RestClient _client;
    private readonly string _endPoint = "https://localhost:7274/api/BlogService";

    public RestClientExample()
    {
        _client = new RestClient();
    }

    public async Task GetBlogs()
    {
        RestRequest request = new RestRequest(_endPoint, Method.Get);
        var response = await _client.GetAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = response.Content;
            Console.WriteLine(jsonStr);
        }
    }

    public async Task Edit(int id)
    {
        RestRequest request = new RestRequest($"{_endPoint}/{id}", Method.Get);
        var response = await _client.GetAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = response.Content;
            Console.WriteLine(jsonStr);
        }
    }

    public async Task Create()
    {
        Console.WriteLine("BlogTitle : ");
        string title = Console.ReadLine()!;
        Console.WriteLine("BlogAuthor : ");
        string author = Console.ReadLine()!;
        Console.WriteLine("BlogContent : ");
        string content = Console.ReadLine()!;
        var blogModel = new blogModel()
        {
            blogTitle = title,
            blogAuthor = author,
            blogContent = content
        };
        RestRequest request = new RestRequest(_endPoint, Method.Post);
        request.AddJsonBody(blogModel);
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = response.Content;
            Console.WriteLine(jsonStr);
        }
    }
    
    public async Task Update()
    {
        Console.WriteLine("BlogId : ");
        int id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("BlogTitle : ");
        string title = Console.ReadLine()!;
        Console.WriteLine("BlogAuthor : ");
        string author = Console.ReadLine()!;
        Console.WriteLine("BlogContent : ");
        string content = Console.ReadLine()!;
        var blogModel = new blogModel()
        {
            blogTitle = title,
            blogAuthor = author,
            blogContent = content
        };
        RestRequest request = new RestRequest($"{_endPoint}/{id}", Method.Patch);
        request.AddJsonBody(blogModel);
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = response.Content;
            Console.WriteLine(jsonStr);
        }
    }
    
    public async Task Delete()
    {
        Console.WriteLine("BlogId : ");
        int id = int.Parse(Console.ReadLine()!);
       
        RestRequest request = new RestRequest($"{_endPoint}/{id}", Method.Delete);
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = response.Content;
            Console.WriteLine(jsonStr);
        }
    }

    public class blogModel
    {
        public int blogId { get; set; }
        public string blogTitle { get; set; }
        public string blogAuthor { get; set; }
        public string blogContent { get; set; }
        public bool deleteFlag { get; set; } = false;
    }
}