// See https://aka.ms/new-console-template for more information

using MYTDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");

// HttpClientExample httpClientExample = new HttpClientExample();
//
// await httpClientExample.Edit(21);
// await httpClientExample.Patch(22, "honey Update", "","" );
// await httpClientExample.Edit(22);


// RestClientExample _client = new RestClientExample();
//
// await _client.GetBlogs();

RefitExample _refit = new RefitExample();
 await _refit.RunAsync();