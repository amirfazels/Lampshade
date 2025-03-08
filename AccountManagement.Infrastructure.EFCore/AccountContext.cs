using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore
{
    public class AccountContext : DbContext
    {
        DbSet<Account> Accounts { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
