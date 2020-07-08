using Microsoft.EntityFrameworkCore;

namespace StockTicker.Service.DAL.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Company { get; set; }
    }
}
