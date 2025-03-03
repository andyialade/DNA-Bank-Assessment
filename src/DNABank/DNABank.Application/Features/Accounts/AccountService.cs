using Microsoft.EntityFrameworkCore;

namespace DNABank.Application.Features.Accounts;

public class AccountService(IAccountFactory accountFactory, IAccountHelper accountHelper, IDNABankDbContext dnaBankDbContext) : IAccountService
{
    public async Task<NewAccountDto> CreateAccount(string fullName, AccountType accountType)
    {
        var generatedAccountNo = accountHelper.GenerateAccountNumber();
        var newAccount = accountFactory.CreateAccount(fullName, generatedAccountNo, accountType);

        await dnaBankDbContext.BankAccounts.AddAsync(newAccount);
        await dnaBankDbContext.SaveChangesAsync(new CancellationToken());

        return new NewAccountDto() { Id = newAccount.Id, AccountNumber = newAccount.AccountNumber, AccountType = accountHelper.GetAccountType(accountType) };
    }

    public async Task<BankAccountDto?> GetAccountById(Guid id)
    {
        var account = await dnaBankDbContext.BankAccounts.FindAsync(id)
            ?? throw new NotFoundException(nameof(BankAccount), id.ToString());

        return AccountMapper.MapToDto(account);
    }

    public async Task<IEnumerable<BankAccountDto>> GetAllAccounts()
    {
        var accounts = await dnaBankDbContext.BankAccounts.ToListAsync();
        if (!accounts.Any()) return Enumerable.Empty<BankAccountDto>();

        return accounts.Select(AccountMapper.MapToDto);
    }

    public async Task<bool> UpdateAccountStatus(Guid id, bool IsFrozen)
    {
        var account = await dnaBankDbContext.BankAccounts.FindAsync(id)
            ?? throw new NotFoundException(nameof(BankAccount), id.ToString());

        account.IsFrozen = IsFrozen;

        dnaBankDbContext.BankAccounts.Update(account);
        await dnaBankDbContext.SaveChangesAsync(new CancellationToken());

        return true;
    }
}
