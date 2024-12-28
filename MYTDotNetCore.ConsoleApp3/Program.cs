// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

HttpClient http = new HttpClient();

var response =  await http.GetAsync("https://jsonplaceholder.typicode.com/posts/55");
if (response.IsSuccessStatusCode)
{
    string jsonStr = await response.Content.ReadAsStringAsync();
    Console.WriteLine(jsonStr);
}