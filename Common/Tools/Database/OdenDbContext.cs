using Microsoft.EntityFrameworkCore;

namespace Common.Tools.Database
{
    public class OdenDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySQL(MySqlConnectionStrings.ConnectionStrings);
    }
}
