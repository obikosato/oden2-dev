using Microsoft.EntityFrameworkCore;

namespace Common.Tools.Database
{
    public class OdenDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySQL(@"server=localhost;database=mydb;userid=root;pwd=password;sslmode=none;");
          //  => optionsBuilder.UseMySQL(MySqlConnectionStrings.ConnectionStrings);
    }
}
