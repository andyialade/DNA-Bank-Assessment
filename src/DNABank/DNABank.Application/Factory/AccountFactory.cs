
namespace DNABank.Application.Factory;

public class AccountFactory(IValidator<SavingsAccount> savingAccValidator, IValidator<CurrentAccount> currentAccValidator) : IAccountFactory
{
    public BankAccount CreateAccount(string fullName, string accountNumber, AccountType accountType)
    {
        switch (accountType)
        {
            case AccountType.Savings:
                var savingAccount = new SavingsAccount()
                {
                    Id = Guid.NewGuid(),
                    FullName = fullName,
                    AccountNumber = accountNumber,
                    AccountType = AccountType.Savings,
                    IsFrozen = false,
                    InterestRate = AccountRates.BaseInterestRate,
                    WithdrawCount = 0
                };
                savingAccValidator.ValidateAndThrow(savingAccount);
                return savingAccount;
            case AccountType.Current:
                var currentAccount = new CurrentAccount()
                {
                    Id = Guid.NewGuid(),
                    FullName = fullName,
                    AccountNumber = accountNumber,
                    AccountType = AccountType.Current,
                    IsFrozen = false,
                    HasOverdraft = false,
                    OverdraftLimit = 0.0M
                };
                currentAccValidator.ValidateAndThrow(currentAccount);
                return currentAccount;
            default:
                throw new ArgumentException($"Invalid Account Type : {accountType}");
        }
    }
}
