namespace DNABank.Application.Mapper;

public static class AccountMapper
{
    public static BankAccountDto MapToDto(BankAccount bankAccount)
    {
        if (bankAccount == null)
        {
            throw new ArgumentNullException(nameof(BankAccount));
        }

        switch (bankAccount.AccountType)
        {
            case AccountType.Current:
                var currentAccount = bankAccount as CurrentAccount;
                return new CurrenAccountDto
                {
                    FullName = currentAccount.FullName,
                    AccountNumber = currentAccount.AccountNumber,
                    IsFrozen = currentAccount.IsFrozen,
                    HasOverdraft = currentAccount.HasOverdraft,
                    OverdraftLimit = currentAccount.OverdraftLimit
                };
            case AccountType.Savings:
                var savingsAccount = bankAccount as SavingsAccount;
                return new SavingsAccountDto
                {
                    FullName = savingsAccount.FullName,
                    AccountNumber = savingsAccount.AccountNumber,
                    IsFrozen = savingsAccount.IsFrozen,
                    InterestRate = savingsAccount.InterestRate,
                    WithdrawCount = savingsAccount.WithdrawCount
                };
            default:
                throw new ArgumentException($"Invalid Account Type : {bankAccount.AccountType}");
        }
    }
}
