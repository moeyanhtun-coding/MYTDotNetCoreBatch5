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
        var response = await _httpClient.GetAsync (_endPoint);
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }

    public async Task GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync(_endPoint + "/" + id);
        if (response.IsSuccessStatusCode)
        {
            var jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }
}