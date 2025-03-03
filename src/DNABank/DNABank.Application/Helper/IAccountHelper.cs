namespace DNABank.Application.Helper;

public interface IAccountHelper
{
    string GenerateAccountNumber();

    string GetAccountType(AccountType accountType);
}
