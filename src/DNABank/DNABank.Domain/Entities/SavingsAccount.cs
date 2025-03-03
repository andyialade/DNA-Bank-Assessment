namespace DNABank.Domain.Entities;

public class SavingsAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public int WithdrawCount { get; set; }
}
