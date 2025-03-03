using DNABank.Application.Persistence;
using DNABank.Domain.Entities;
using DNABank.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DNABank.Infrastructure.Persistence;

public class DNABankDbContext : DbContext, IDNABankDbContext
{
    public DNABankDbContext(DbContextOptions<DNABankDbContext> options) : base(options) { }

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SavingsAccount>().HasBaseType<BankAccount>();
        modelBuilder.Entity<CurrentAccount>().HasBaseType<BankAccount>();

        base.OnModelCreating(modelBuilder);
    }
}
