namespace DNABank.Application.Features.Accounts;

public interface IAccountService
{
    Task<NewAccountDto> CreateAccount(string fullName, AccountType accountType);

    Task<BankAccountDto?> GetAccountById(Guid id);

    Task<IEnumerable<BankAccountDto>> GetAllAccounts();

    Task<bool> UpdateAccountStatus(Guid id, bool IsFrozen);
}
