// See https://aka.ms/new-console-template for more information

using MYTDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");

HttpClientExample httpClientExample = new HttpClientExample();

await httpClientExample.GetByIdAsync(12);
