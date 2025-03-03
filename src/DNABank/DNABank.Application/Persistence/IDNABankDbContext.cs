using Microsoft.EntityFrameworkCore;

namespace DNABank.Application.Persistence;

public interface IDNABankDbContext
{
    DbSet<BankAccount> BankAccounts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
