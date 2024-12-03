using Microsoft.EntityFrameworkCore;

namespace MYTDotNetCore.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=.; Initial Catalog = MYTDotNetCoreBatch5; User ID=sa; Password=sasa@123;"
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}

