// See https://aka.ms/new-console-template for more information
using MYTDotNetCore.Database.Model;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
TblBlog blog = new TblBlog
{
    BlogId = 1,
    BlogTitle = "title",
    BlogAuthor = "Author",
    BlogContent = "content",
};
string jsonStr = blog.ToJson();
string jsonStr2 = """{"BlogId":1,"BlogTitle":"title","BlogAuthor":"Author","BlogContent":"content","DeleteFlag":null}""";

//string jsonStr = JsonConvert.SerializeObject(blog, Formatting.Indented);
Console.WriteLine(jsonStr);
var Blog = JsonConvert.DeserializeObject<TblBlog>(jsonStr2);
Console.WriteLine(Blog.BlogTitle);

public static class Extensions
{
    public static string ToJson(this object obj, bool format = false )
    {
        var jsonStr = JsonConvert.SerializeObject(obj , format ? Formatting.Indented : Formatting.None);
        return jsonStr;
    }

    public static object ToObject(this string str)
    {
        var obj = JsonConvert.DeserializeObject<object>(str)!;
        return obj;
    }

}
