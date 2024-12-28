using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace MYTDotNetCore.ConsoleApp3;

public class HttpClientExample
{
    private readonly HttpClient _httpClient;
    private readonly string _endPoint = "https://localhost:7274/api/Blogs";

    public HttpClientExample()
    {
        _httpClient = new HttpClient();
    }

    public async Task ReadAsync()
    {
        var response = await _httpClient.GetAsync(_endPoint);
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }

    public async Task Edit(int id)
    {
        var response = await _httpClient.GetAsync($"{_endPoint}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        if (response.IsSuccessStatusCode)
        {
            var jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }

    public async Task Create(string name, string author, string content)
    {
        blogModel blog = new()
        {
            blogTitle = name,
            blogAuthor = author,
            blogContent = content
        };

        string jsonRequest = JsonConvert.SerializeObject(blog, Formatting.Indented);
        var stringContent = new StringContent(jsonRequest, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync(_endPoint, stringContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task Patch(int id, string name, string author, string content)
    {
        blogModel blog = new()
        {
            blogTitle = name,
            blogAuthor = author,
            blogContent = content
        };
        string jsonRequest = JsonConvert.SerializeObject(blog, Formatting.Indented);
        var stringContent = new StringContent(jsonRequest, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _httpClient.PatchAsync($"{_endPoint}/{id}", stringContent);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("No Data Found");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_endPoint}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        if (response.IsSuccessStatusCode)
            Console.WriteLine(await response.Content.ReadAsStringAsync());
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