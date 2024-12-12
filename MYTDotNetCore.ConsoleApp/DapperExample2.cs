using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MYTDotNetCore.ConsoleApp.Models;
using MYTDotNetCore.Shared;

namespace MYTDotNetCore.ConsoleApp
{
    public class DapperExample2
    {
        private readonly string _connectionString = "Data Source=.; Initial Catalog=MYTDotNetCoreBatch5; User Id=sa; Password=sasa@123";
        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {
            string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0";
            var lst = _dapperService.Query<BlogDapperDataModel>(query);
            foreach (var item in lst)
            {
                Console.WriteLine("BlogId: " + item.BlogId);
                Console.WriteLine("BlogTitle: " + item.BlogTitle);
                Console.WriteLine("BlogAuthor: " + item.BlogAuthor);
                Console.WriteLine("BlogContent: " + item.BlogContent);
            }
        }
    }
}
