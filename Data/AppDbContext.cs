using Microsoft.EntityFrameworkCore;
using TransactionIngest.Models;

namespace TransactionIngest.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionAudit> Audits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=transactions.db");
        }
    }
}