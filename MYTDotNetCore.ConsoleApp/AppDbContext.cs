using System.Data;
using Microsoft.EntityFrameworkCore;
using MYTDotNetCore.ConsoleApp.Models;

namespace MYTDotNetCore.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=.; Initial Catalog = MYTDotNetCoreBatch5; User ID=sa; Password=sasa@123; TrustServerCertificate = true";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}

