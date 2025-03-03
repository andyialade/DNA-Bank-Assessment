namespace DNABank.Application.Factory;

public interface IAccountFactory
{
    BankAccount CreateAccount(string fullName, string accountNumber, AccountType accountType);
}
